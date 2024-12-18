using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace Engine
{
	public class TXTFileLoader
	{
		public delegate void OnFinishLoadTXTCall(string content);

		public delegate void OnFinishLoadBytesCall(byte[] bytes, object data);

		private sealed class _LoadTXT_d__8 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int __1__state;

			private object __2__current;

			public string url;

			public TXTFileLoader __4__this;

			private WWW _www_5__2;

			object IEnumerator<object>.Current
			{
				get
				{
					return this.__2__current;
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return this.__2__current;
				}
			}

			public _LoadTXT_d__8(int __1__state)
			{
				this.__1__state = __1__state;
			}

			void IDisposable.Dispose()
			{
			}

			bool IEnumerator.MoveNext()
			{
				int num = this.__1__state;
				TXTFileLoader tXTFileLoader = this.__4__this;
				if (num == 0)
				{
					this.__1__state = -1;
					this._www_5__2 = new WWW(this.url);
					this.__2__current = this._www_5__2;
					this.__1__state = 1;
					return true;
				}
				if (num != 1)
				{
					return false;
				}
				this.__1__state = -1;
				if (this._www_5__2.error != null)
				{
					UnityEngine.Debug.Log("error = " + this._www_5__2.error);
				}
				else if (tXTFileLoader.m_finishTXTCall.ContainsKey(this.url) && tXTFileLoader.m_finishTXTCall[this.url] != null)
				{
					tXTFileLoader.m_finishTXTCall[this.url](this._www_5__2.text);
					tXTFileLoader.m_finishTXTCall.Remove(this.url);
				}
				return false;
			}

			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		private sealed class _LoadBytes_d__10 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int __1__state;

			private object __2__current;

			public string url;

			public TXTFileLoader __4__this;

			public object data;

			private WWW _www_5__2;

			object IEnumerator<object>.Current
			{
				get
				{
					return this.__2__current;
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return this.__2__current;
				}
			}

			public _LoadBytes_d__10(int __1__state)
			{
				this.__1__state = __1__state;
			}

			void IDisposable.Dispose()
			{
			}

			bool IEnumerator.MoveNext()
			{
				int num = this.__1__state;
				TXTFileLoader tXTFileLoader = this.__4__this;
				if (num == 0)
				{
					this.__1__state = -1;
					this._www_5__2 = new WWW(this.url);
					this.__2__current = this._www_5__2;
					this.__1__state = 1;
					return true;
				}
				if (num != 1)
				{
					return false;
				}
				this.__1__state = -1;
				if (this._www_5__2.error != null)
				{
					UnityEngine.Debug.Log("error = " + this._www_5__2.error);
				}
				else if (tXTFileLoader.m_finishBytesCall.ContainsKey(this.url) && tXTFileLoader.m_finishBytesCall[this.url] != null)
				{
					byte[] bytes = new byte[this._www_5__2.bytes.Length];
					bytes = SystemManager.DecryptBundleFile(this._www_5__2.bytes);
					tXTFileLoader.m_finishBytesCall[this.url](bytes, this.data);
					tXTFileLoader.m_finishBytesCall.Remove(this.url);
				}
				return false;
			}

			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		public Dictionary<string, TXTFileLoader.OnFinishLoadTXTCall> m_finishTXTCall = new Dictionary<string, TXTFileLoader.OnFinishLoadTXTCall>();

		public Dictionary<string, TXTFileLoader.OnFinishLoadBytesCall> m_finishBytesCall = new Dictionary<string, TXTFileLoader.OnFinishLoadBytesCall>();

		private static TXTFileLoader m_instance;

		public static TXTFileLoader Instance
		{
			get
			{
				if (TXTFileLoader.m_instance == null)
				{
					TXTFileLoader.m_instance = new TXTFileLoader();
				}
				return TXTFileLoader.m_instance;
			}
		}

		public void LoadTXTFileByName(TXTFileLoader.OnFinishLoadTXTCall callback, string url, string encode = "utf-8")
		{
			if (this.m_finishTXTCall.ContainsKey(url))
			{
				this.m_finishTXTCall[url] = callback;
			}
			else
			{
				this.m_finishTXTCall.Add(url, callback);
			}
			if (SystemManager.CalculateString(url) > 0 && !SystemManager.CheckIsMobilePlatform())
			{
				string content = new StreamReader(new FileStream(url.Substring(url.IndexOf("://") + 3), FileMode.Open, FileAccess.Read), Encoding.GetEncoding(encode)).ReadToEnd();
				this.m_finishTXTCall[url](content);
				this.m_finishTXTCall.Remove(url);
				return;
			}
			MonoManager.Instance.StartCoroutine(this.LoadTXT(url));
		}

//		[IteratorStateMachine(typeof(TXTFileLoader._003CLoadTXT_003Ed__8))]
		private IEnumerator LoadTXT(string url)
		{
			int num = 0;
			WWW wWW = null;
			while (num == 0)
			{
				wWW = new WWW(url);
				yield return wWW;
			}
			if (num != 1)
			{
				yield break;
			}
			if (wWW.error != null)
			{
				UnityEngine.Debug.Log("error = " + wWW.error);
			}
			else if (this.m_finishTXTCall.ContainsKey(url) && this.m_finishTXTCall[url] != null)
			{
				this.m_finishTXTCall[url](wWW.text);
				this.m_finishTXTCall.Remove(url);
			}
			yield break;
		}

		public void LoadBytesByPath(TXTFileLoader.OnFinishLoadBytesCall callback, string url, object data = null)
		{
			if (this.m_finishBytesCall.ContainsKey(url))
			{
				this.m_finishBytesCall[url] = callback;
			}
			else
			{
				this.m_finishBytesCall.Add(url, callback);
			}
			if (SystemManager.CalculateString(url) > 0 && !SystemManager.CheckIsMobilePlatform())
			{
				FileStream expr_54 = new FileStream(url.Substring(url.IndexOf("://") + 3), FileMode.Open, FileAccess.Read);
				int num = (int)expr_54.Length;
				byte[] array = new byte[num];
				expr_54.Read(array, 0, num);
				array = SystemManager.DecryptBundleFile(array);
				this.m_finishBytesCall[url](array, data);
				this.m_finishBytesCall.Remove(url);
				return;
			}
			MonoManager.Instance.StartCoroutine(this.LoadBytes(url, data));
		}

//		[IteratorStateMachine(typeof(TXTFileLoader._003CLoadBytes_003Ed__10))]
		private IEnumerator LoadBytes(string url, object data = null)
		{
			int num = 0;
			WWW wWW = null;
			while (num == 0)
			{
				wWW = new WWW(url);
				yield return wWW;
			}
			if (num != 1)
			{
				yield break;
			}
			if (wWW.error != null)
			{
				UnityEngine.Debug.Log("error = " + wWW.error);
			}
			else if (this.m_finishBytesCall.ContainsKey(url) && this.m_finishBytesCall[url] != null)
			{
				byte[] bytes = new byte[wWW.bytes.Length];
				bytes = SystemManager.DecryptBundleFile(wWW.bytes);
				this.m_finishBytesCall[url](bytes, data);
				this.m_finishBytesCall.Remove(url);
			}
			yield break;
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Commonder;
using UnityEngine;

namespace Pathea
{
    public class AssetMgrCmd : MonoBehaviour, ICmd
    {
        public void Start()
        {
            Cmd.Instance.Register(this);
        }

        public void OnDestroy()
        {
            Cmd.Instance.ClearAll();
        }

        [Command("AssetMgr", "AsyncInstantiate", "异步实例化资源", false)]
        public void AsyncInstantiate(string bundleName, string assetName)
        {
            AsyncInstantiator.Instance.Add(bundleName, assetName, delegate (GameObject gameObject)
            {
            });
        }

        [Command("AssetMgr", "PreloadAllAssetsFromBundle", "预加载bundle里的所有资源", false)]
        public void PreloadAllAssetsFromBundle(string bundleName)
        {
            Singleton<CoroutineMgr>.Instance.StartCoroutine(LoadAllAssets(bundleName));
        }

        [Command("AssetMgr", "CompareLoad", "对比资源加载", false)]
        public void CompareLoad(string bundleName)
        {
            Singleton<CoroutineMgr>.Instance.StartCoroutine(CompareCoroutine(bundleName));
        }

        public IEnumerator CompareCoroutine(string bundleName)
        {
            yield return LoadAllAssets(bundleName);
            yield return LoadAssets(bundleName);
            yield return LoadAllAssets(bundleName);
            assetNames.Reverse();
            yield return LoadAssets(bundleName);
            yield break;
        }

        public static int count;

        public IEnumerator LoadAssets(string bundleName)
        {
            RealtimeTimer timer = new RealtimeTimer();
            timer.Toggle();
            yield return Resources.UnloadUnusedAssets();
            yield return new WaitForSeconds(5f);
            timer.Flag("unload unused assets");
            AssetProvider assetProvider = AssetMgr.CreateAssetProvider(bundleName, 0, false);
            timer.Flag("create provider");
            foreach (string assetName in assetNames)
            {
                LoadAssetOperation<UnityEngine.Object> req = assetProvider.LoadAssetAsync<UnityEngine.Object>(assetName, 1000);
                req.Completed += delegate (Operation op)
                {
                    req.GetAsset();
                    count++;
                };
            }
            yield return new WaitUntil(() => count == assetNames.Count);
            timer.Flag(string.Format("end load assets:{0}", assetNames.Count));
            timer.Log("end load assets from bundle:" + bundleName);
            yield break;
        }

        public IEnumerator LoadAllAssets(string bundleName)
        {
            RealtimeTimer timer = new RealtimeTimer();
            timer.Toggle();
            yield return Resources.UnloadUnusedAssets();
            yield return new WaitForSeconds(5f);
            timer.Flag("unload unused assets");
            AssetProvider provider = AssetMgr.CreateAssetProvider(bundleName, 0, false);
            timer.Flag("create provider");
            LoadAllAssetOperation operation = provider.LoadAllAssetAsync<UnityEngine.Object>();
            timer.Flag("create load operation");
            yield return operation;
            timer.Flag("load complete");
            UnityEngine.Object[] allAsset = operation.GetAllAsset();
            timer.Flag("get all asset");
            AssetMgr.DestroyAssetProvider(provider);
            timer.Flag("destroy provider");
            timer.Flag("log out");
            timer.Log("load all assets from bundle:" + bundleName);
            assetNames.Clear();
            foreach (UnityEngine.Object @object in allAsset)
            {
                assetNames.Add(@object.name);
            }
            yield break;
        }

        [Command("AssetMgr", "StartCacheAssetLoad", "开始缓存加载好的资源", false)]
        public void StartCacheAssetLoad()
        {
            AssetMgr.StartCacheAsset(500);
        }

        [Command("AssetMgr", "StopCacheAssetLoad", "停止缓存加载好的资源", false)]
        public void StopCacheAssetLoad()
        {
            AssetMgr.StopCacheAsset();
        }

        [Command("AssetMgr", "MaxUpdatingAssetOperation", "设置同时向系统请求的资源数", false)]
        public void MaxUpdatingAssetOperation(int count)
        {
            AssetMgr.MaxUpdatingAssetOperation = count;
        }

        public AssetMgrCmd()
        {
        }

        public List<string> assetNames = new List<string>(3000);

    }
}

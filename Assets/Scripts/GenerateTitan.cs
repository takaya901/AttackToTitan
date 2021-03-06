﻿using System;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>巨人を生成するクラス</summary>
// https://gametukurikata.com/customize/editor/setgameobject
public class GenerateTitan : MonoBehaviour
{
    [SerializeField] GameObject _titan;      //巨人
    [SerializeField] Terrain _terrain;       //フィールド
    [SerializeField] int _titansNum = 20;    //巨人の数
    [SerializeField] float _maxHeight = 5;   //巨人の大きさの上限
    [SerializeField] float _minHeight = 1;   //巨人の大きさの下限
    [SerializeField] float _minDistance = 2; //巨人同士の最小間隔
    
    const float MAX_XZ = 300f;   //生成位置のx,z座標の最大値
    const float NO_GEN_AREA = 100f; //壁の中心からこの距離内には生成しない
        
    void Start ()
    {
        //指定された数だけ巨人を生成する
        for (int i = 0; i < _titansNum; i++) 
        {
            Vector3 genPos; //巨人の生成位置

            //生成位置のx,z座標をランダムに決定
            do {
                genPos = new Vector3(Random.Range(-MAX_XZ, MAX_XZ), 0, Random.Range(-MAX_XZ, MAX_XZ));
            } while (Math.Abs(genPos.x) < NO_GEN_AREA && Math.Abs(genPos.z) < NO_GEN_AREA);   //壁内と壁のすぐ近くには生成しない

            var rayOriginY = _terrain.GetPosition().y + _terrain.terrainData.size.y; //rayの開始地点のy座標．terrainの最も高い位置に設定
            var downwardRay = new Ray(new Vector3(genPos.x, rayOriginY, genPos.z), Vector3.down);    //terrainとの衝突判定のための下向きray
            RaycastHit terrainHit;  //terrainとの衝突情報

            //地面との接触位置を取得
            if (!Physics.Raycast(downwardRay, out terrainHit, _terrain.terrainData.size.y, LayerName.FieldMask)) {
                return;
            }
            genPos = terrainHit.point;  //接触位置を保存

            //指定半径内に他のTitanがいなければ，地面の接触ポイントにランダムな大きさで敵を配置
            if (!Physics.SphereCast(downwardRay, _minDistance, rayOriginY, LayerName.TitanMask)) {
                var titanScale = Random.Range(_minHeight, _maxHeight);
                Instantiate(_titan, genPos, Quaternion.identity).transform.localScale = new Vector3(titanScale, titanScale, titanScale);
            }
        }
    }
}

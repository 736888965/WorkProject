/*
 * Tencent is pleased to support the open source community by making xLua available.
 * Copyright (C) 2016 THL A29 Limited, a Tencent company. All rights reserved.
 * Licensed under the MIT License (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
 * http://opensource.org/licenses/MIT
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
*/

using UnityEngine;
using XLua;

namespace XLuaTest
{
    public class Helloworld : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            LuaEnv luaenv = new LuaEnv();
            luaenv.DoString("CS.UnityEngine.Debug.Log('hello world')");
            luaenv.DoString("CS.UnityEngine.Debug.LogError('hello world')");
            //加载lua文件
            luaenv.DoString("require 'byfile'");
            // 加载自定义文件  loader
            luaenv.AddLoader((ref string fileName) =>
            {
                if (fileName == "test")
                {
                    string script = "return {ccc = 9999}";
                    return System.Text.Encoding.UTF8.GetBytes(script);
                }
                return null;
            });

            luaenv.DoString("print('test.ccc=',require('test').ccc)");

            luaenv.Dispose();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

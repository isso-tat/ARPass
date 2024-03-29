//-----------------------------------------------------------------------
// <copyright file="PointCloudManager.cs" company="Google">
//
// Copyright 2019 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace GoogleARCore.InstantPreview.Editor
{
    using System;
    using System.Collections.Generic;
    using GoogleARCore;
    using UnityEngine;

    internal class PointCloudManager
    {
        private NativeSession m_NativeSession = null;

        private float m_LastReleasedPointcloudTimestamp = 0.0f;

        public PointCloudManager(NativeSession session)
        {
            m_NativeSession = session;
        }

        public IntPtr PointCloudHandle { get; private set; }

        public bool IsPointCloudNew
        {
            get
            {
                return m_NativeSession.PointCloudApi.GetTimestamp(PointCloudHandle) !=
                    m_LastReleasedPointcloudTimestamp;
            }
        }

        public void OnUpdate()
        {
#if UNITY_EDITOR || UNITY_ANDROID
            // After first frame, release previous frame's point cloud.
            if (PointCloudHandle != IntPtr.Zero)
            {
                m_LastReleasedPointcloudTimestamp = m_NativeSession.PointCloudApi.GetTimestamp(PointCloudHandle);
                m_NativeSession.PointCloudApi.Release(PointCloudHandle);
                PointCloudHandle = IntPtr.Zero;
            }

            IntPtr pointCloudHandle;
            m_NativeSession.FrameApi.TryAcquirePointCloudHandle(out pointCloudHandle);
            PointCloudHandle = pointCloudHandle;
#endif
        }
    }
}

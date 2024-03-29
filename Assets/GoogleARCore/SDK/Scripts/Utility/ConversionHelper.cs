//-----------------------------------------------------------------------
// <copyright file="ConversionHelper.cs" company="Google">
//
// Copyright 2016 Google Inc. All Rights Reserved.
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
    using UnityEngine;

    internal class ConversionHelper
    {
        private static readonly Matrix4x4 k_UnityWorldToGLWorld
            = Matrix4x4.Scale(new Vector3(1, 1, -1));

        private static readonly Matrix4x4 k_UnityWorldToGLWorldInverse
            = k_UnityWorldToGLWorld.inverse;

        public static void UnityPoseToApiPose(Pose unityPose, out ApiPoseData apiPose)
        {
            Matrix4x4 glWorld_T_glLocal =
                Matrix4x4.TRS(unityPose.position, unityPose.rotation, Vector3.one);
            Matrix4x4 unityWorld_T_unityLocal =
                k_UnityWorldToGLWorld * glWorld_T_glLocal * k_UnityWorldToGLWorldInverse;

            Vector3 position = unityWorld_T_unityLocal.GetColumn(3);
            Quaternion rotation = Quaternion.LookRotation(unityWorld_T_unityLocal.GetColumn(2),
                unityWorld_T_unityLocal.GetColumn(1));

            apiPose.X = position.x;
            apiPose.Y = position.y;
            apiPose.Z = position.z;
            apiPose.Qx = rotation.x;
            apiPose.Qy = rotation.y;
            apiPose.Qz = rotation.z;
            apiPose.Qw = rotation.w;
        }

        public static void ApiPoseToUnityPose(ApiPoseData apiPose, out Pose unityPose)
        {
            Matrix4x4 glWorld_T_glLocal =
                Matrix4x4.TRS(
                    new Vector3(apiPose.X, apiPose.Y, apiPose.Z),
                    new Quaternion(apiPose.Qx, apiPose.Qy, apiPose.Qz, apiPose.Qw), Vector3.one);
            Matrix4x4 unityWorld_T_unityLocal =
                k_UnityWorldToGLWorld * glWorld_T_glLocal * k_UnityWorldToGLWorldInverse;

            Vector3 position = unityWorld_T_unityLocal.GetColumn(3);
            Quaternion rotation = Quaternion.LookRotation(unityWorld_T_unityLocal.GetColumn(2),
                unityWorld_T_unityLocal.GetColumn(1));

            unityPose = new Pose(position, rotation);
        }

        public static void ApiVectorToUnityVector(float[] ApiVector, out Vector3 unityVector)
        {
            unityVector = k_UnityWorldToGLWorld * new Vector3(
                ApiVector[0], ApiVector[1], ApiVector[2]);
        }
    }
}

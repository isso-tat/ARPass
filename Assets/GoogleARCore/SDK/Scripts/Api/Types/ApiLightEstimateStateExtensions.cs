﻿//-----------------------------------------------------------------------
// <copyright file="ApiLightEstimateStateExtensions.cs" company="Google">
//
// Copyright 2017 Google Inc. All Rights Reserved.
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
    using GoogleARCore;

    internal static class ApiLightEstimateStateExtensions
    {
        public static LightEstimateState ToLightEstimateState(this ApiLightEstimateState apiState)
        {
            switch (apiState)
            {
                case ApiLightEstimateState.NotValid:
                    return LightEstimateState.NotValid;
                case ApiLightEstimateState.Valid:
                    return LightEstimateState.Valid;
                default:
                    return LightEstimateState.NotValid;
            }
        }
    }
}

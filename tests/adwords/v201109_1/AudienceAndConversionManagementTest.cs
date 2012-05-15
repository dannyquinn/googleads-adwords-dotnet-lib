﻿// Copyright 2012, Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201109_1;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

using CSharpExamples = Google.Api.Ads.AdWords.Examples.CSharp.v201109_1;
using VBExamples = Google.Api.Ads.AdWords.Examples.VB.v201109_1;

namespace Google.Api.Ads.AdWords.Tests.v201109_1 {
  /// <summary>
  /// Test cases for all the code examples under
  /// v201109_1\AudienceAndConversionManagement.
  /// </summary>
  class AudienceAndConversionManagementTest : ExampleBaseTests {
    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    public void Init() {
      parameters = new Dictionary<string, string>();
    }

    /// <summary>
    /// Tests the AddAudience VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddAudienceVBExample() {
      RunExample(new VBExamples.AddAudience());
    }

    /// <summary>
    /// Tests the AddAudience C# code example.
    /// </summary>
    [Test]
    public void TestAddAudienceCSharpExample() {
      RunExample(new CSharpExamples.AddAudience());
    }

    /// <summary>
    /// Tests the AddConversionTracker VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddConversionTrackerVBExample() {
      RunExample(new VBExamples.AddConversionTracker());
    }

    /// <summary>
    /// Tests the AddConversionTracker C# code example.
    /// </summary>
    [Test]
    public void TestAddConversionTrackerCSharpExample() {
      RunExample(new CSharpExamples.AddConversionTracker());
    }
  }
}
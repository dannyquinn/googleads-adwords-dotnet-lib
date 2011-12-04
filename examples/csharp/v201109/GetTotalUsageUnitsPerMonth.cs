// Copyright 2011, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201109;

using System;
using System.IO;
using System.Net;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109 {
  /// <summary>
  /// This code example retrieves the total number of API units for this entire
  /// month that can be used by the developer token being used to make
  /// this call.
  ///
  /// Tags: InfoService.get
  /// </summary>
  class GetTotalUsageUnitsPerMonth : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example retrieves the total number of API units for this entire" +
            " month that can be used by the developer token being used to make this call.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetTotalUsageUnitsPerMonth();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the InfoService.
      InfoService infoService = (InfoService) user.GetService(AdWordsService.v201109.InfoService);

      InfoSelector selector = new InfoSelector();
      selector.apiUsageType = ApiUsageType.TOTAL_USAGE_API_UNITS_PER_MONTH;

      try {
        ApiUsageInfo info = infoService.get(selector);
        if (info != null) {
          Console.WriteLine("The total number of API units available this month is '{0}'.",
              info.cost);
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve API usage info. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
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
using Google.Api.Ads.AdWords.v201101;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Google.Api.Ads.AdWords.Tests.v201101 {
  /// <summary>
  /// UnitTests for <see cref="BulkMutateJobService"/> class.
  /// </summary>
  [TestFixture]
  class BulkMutateJobServiceTests : BaseTests {
    /// <summary>
    /// AdGroupAdService object to be used in this test.
    /// </summary>
    private BulkMutateJobService bulkMutateJobService;

    /// <summary>
    /// The campaign id for which tests are run.
    /// </summary>
    private long campaignId = 0;

    /// <summary>
    /// The adgroup id for which tests are run.
    /// </summary>
    private long adGroupId = 0;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public BulkMutateJobServiceTests() : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      bulkMutateJobService = (BulkMutateJobService) user.GetService(
          AdWordsService.v201101.BulkMutateJobService);
      TestUtils utils = new TestUtils();
      campaignId = utils.CreateCampaign(user, new ManualCPC());
      adGroupId = utils.CreateAdGroup(user, campaignId);
    }

    /// <summary>
    /// Test whether we can set campaign targets using single part job with
    /// single stream and multiple operations.
    /// </summary>
    [Test]
    public void TestSinglePartSingleStreamMultipleOperations() {
      AdGroupAdOperation[] adGroupAdOperations = new AdGroupAdOperation[120];
      for (int i = 0; i < 120; i++) {
        // Create an AdGroupAdOperation to add a text ad.
        AdGroupAdOperation adGroupAdOperation = new AdGroupAdOperation();
        adGroupAdOperation.@operator = Operator.ADD;

        TextAd textAd = new TextAd();
        textAd.headline = "Luxury Cruise to Mars";
        textAd.description1 = string.Format("Visit the Red Planet {0} times.", i + 1);
        textAd.description2 = "Low-gravity fun for everyone!";
        textAd.displayUrl = "www.example.com";
        textAd.url = "http://www.example.com";

        AdGroupAd adGroupAd = new AdGroupAd();
        adGroupAd.adGroupId = adGroupId;
        adGroupAd.ad = textAd;

        adGroupAdOperation.operand = adGroupAd;
        adGroupAdOperations[i] = adGroupAdOperation;
      }

      OperationStream opStream = new OperationStream();

      opStream.scopingEntityId = new EntityId();
      opStream.scopingEntityId.type = EntityIdType.CAMPAIGN_ID;
      opStream.scopingEntityId.value = campaignId;

      opStream.operations = adGroupAdOperations;

      BulkMutateJob bulkJob = null;

      bulkJob = new BulkMutateJob();
      bulkJob.numRequestParts = 1;

      // b. Create a part of the job.

      BulkMutateRequest bulkRequest = new BulkMutateRequest();
      bulkRequest.partIndex = 0;
      bulkRequest.operationStreams = new OperationStream[] {opStream};
      bulkJob.request = bulkRequest;

      // c. Create job operation.
      JobOperation jobOperation = new JobOperation();
      jobOperation.@operator = Operator.ADD;
      jobOperation.operand = bulkJob;

      bulkJob = bulkMutateJobService.mutate(jobOperation);

      bool completed = false;

      while (completed == false) {
        Thread.Sleep(2000);

        BulkMutateJobSelector selector = new BulkMutateJobSelector();
        selector.jobIds = new long[] {bulkJob.id};

        BulkMutateJob[] allJobs = bulkMutateJobService.get(selector);
        if (allJobs != null && allJobs.Length > 0) {
          if (allJobs[0].status == BasicJobStatus.COMPLETED ||
              allJobs[0].status == BasicJobStatus.FAILED) {
            completed = true;
            bulkJob = allJobs[0];
            Assert.Pass();
            break;
          }
        }
      }
    }

    /// <summary>
    /// Test whether we can set campaign targets using single part job with
    /// single stream and multiple operations.
    /// </summary>
    [Test]
    public void TestSinglePartMultipleStreamsSingleOperation() {
      OperationStream[] operationStreams = new OperationStream[6];

      for (int i = 0; i < 6; i++) {
        AdGroupAdOperation[] operations = new AdGroupAdOperation[20];
        for (int j = 0; j < 20; j++) {
          // Create an AdGroupAdOperation to add a text ad.
          AdGroupAdOperation operation = new AdGroupAdOperation();
          operation.@operator = Operator.ADD;

          TextAd textAd = new TextAd();
          textAd.headline = "Luxury Cruise to Mars";
          textAd.description1 = string.Format("Visit the Red Planet {0} times.", i + 1);
          textAd.description2 = "Low-gravity fun for everyone!";
          textAd.displayUrl = "www.example.com";
          textAd.url = "http://www.example.com";

          AdGroupAd adGroupAd = new AdGroupAd();
          adGroupAd.adGroupId = adGroupId;
          adGroupAd.ad = textAd;

          operation.operand = adGroupAd;
          operations[j] = operation;
        }
        OperationStream operationStream = new OperationStream();

        operationStream.scopingEntityId = new EntityId();
        operationStream.scopingEntityId.type = EntityIdType.CAMPAIGN_ID;
        operationStream.scopingEntityId.value = campaignId;

        operationStream.operations = operations;
        operationStreams[i] = operationStream;
      }

      BulkMutateJob bulkJob = null;

      bulkJob = new BulkMutateJob();
      bulkJob.numRequestParts = 1;

      // b. Create a part of the job.

      BulkMutateRequest bulkRequest = new BulkMutateRequest();
      bulkRequest.partIndex = 0;
      bulkRequest.operationStreams = operationStreams;
      bulkJob.request = bulkRequest;

      // c. Create job operation.
      JobOperation jobOperation = new JobOperation();
      jobOperation.@operator = Operator.ADD;
      jobOperation.operand = bulkJob;

      bulkJob = bulkMutateJobService.mutate(jobOperation);

      bool completed = false;

      while (completed == false) {
        Thread.Sleep(2000);

        BulkMutateJobSelector selector = new BulkMutateJobSelector();
        selector.jobIds = new long[] {bulkJob.id};

        BulkMutateJob[] allJobs = bulkMutateJobService.get(selector);
        if (allJobs != null && allJobs.Length > 0) {
          if (allJobs[0].status == BasicJobStatus.COMPLETED ||
              allJobs[0].status == BasicJobStatus.FAILED) {
            completed = true;
            bulkJob = allJobs[0];
            Assert.Pass();
            break;
          }
        }
      }
    }
  }
}
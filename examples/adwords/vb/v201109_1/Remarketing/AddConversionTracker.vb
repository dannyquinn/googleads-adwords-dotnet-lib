' Copyright 2012, Google Inc. All Rights Reserved.
'
' Licensed under the Apache License, Version 2.0 (the "License");
' you may not use this file except in compliance with the License.
' You may obtain a copy of the License at
'
'     http://www.apache.org/licenses/LICENSE-2.0
'
' Unless required by applicable law or agreed to in writing, software
' distributed under the License is distributed on an "AS IS" BASIS,
' WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
' See the License for the specific language governing permissions and
' limitations under the License.

' Author: api.anash@gmail.com (Anash P. Oommen)

Imports Google.Api.Ads.AdWords.Lib
Imports Google.Api.Ads.AdWords.v201109_1

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109_1
  ''' <summary>
  ''' This code example adds an AdWords conversion tracker.
  '''
  ''' Tags: ConversionTrackerService.mutate
  ''' </summary>
  Public Class AddConversionTracker
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New AddConversionTracker
      Console.WriteLine(codeExample.Description)
      Try
        codeExample.Run(New AdWordsUser, codeExample.GetParameters, Console.Out)
      Catch ex As Exception
        Console.WriteLine("An exception occurred while running this code example. {0}", _
            ExampleUtilities.FormatException(ex))
      End Try
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    '''
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example adds an AdWords conversion tracker."
      End Get
    End Property

    ''' <summary>
    ''' Gets the list of parameter names required to run this code example.
    ''' </summary>
    ''' <returns>
    ''' A list of parameter names for this code example.
    ''' </returns>
    Public Overrides Function GetParameterNames() As String()
      Return New String() {}
    End Function

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="parameters">The parameters for running the code
    ''' example.</param>
    ''' <param name="writer">The stream writer to which script output should be
    ''' written.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser, ByVal parameters As  _
        Dictionary(Of String, String), ByVal writer As TextWriter)
      ' Get the ConversionTrackerService.
      Dim conversionTrackerService As ConversionTrackerService = user.GetService( _
          AdWordsService.v201109_1.ConversionTrackerService)

      ' Create AdWords conversion tracker.
      Dim conversionTracker As New AdWordsConversionTracker
      conversionTracker.name = "Earth to Mars Cruises Conversion #" & _
          ExampleUtilities.GetTimeStamp
      conversionTracker.category = ConversionTrackerCategory.DEFAULT
      conversionTracker.markupLanguage = AdWordsConversionTrackerMarkupLanguage.HTML
      conversionTracker.httpProtocol = AdWordsConversionTrackerHttpProtocol.HTTP
      conversionTracker.textFormat = AdWordsConversionTrackerTextFormat.HIDDEN

      ' Set optional fields.
      conversionTracker.status = ConversionTrackerStatus.ENABLED
      conversionTracker.viewthroughLookbackWindow = 15
      conversionTracker.viewthroughConversionDeDupSearch = True
      conversionTracker.isProductAdsChargeable = True
      conversionTracker.productAdsChargeableConversionWindow = 15
      conversionTracker.conversionPageLanguage = "en"
      conversionTracker.backgroundColor = "#0000FF"
      conversionTracker.userRevenueValue = "someJavascriptVariable"

      ' Create the operation.
      Dim operation As New ConversionTrackerOperation
      operation.operator = [Operator].ADD
      operation.operand = conversionTracker

      Try
        ' Add conversion tracker.
        Dim retval As ConversionTrackerReturnValue = conversionTrackerService.mutate( _
            New ConversionTrackerOperation() {operation})

        ' Display the results.
        If ((Not retval Is Nothing) AndAlso (Not retval.value Is Nothing) AndAlso _
            retval.value.Length > 0) Then
          Dim newConversionTracker As ConversionTracker = retval.value(0)
          writer.WriteLine("Conversion tracker with id '{0}', name '{1}', status '{2}', " & _
              "category '{3}' was added.", newConversionTracker.id, newConversionTracker.name, _
              newConversionTracker.status, newConversionTracker.category)
        Else
          writer.WriteLine("No conversion trackers were added.")
        End If
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to add conversion tracker.", ex)
      End Try
    End Sub
  End Class
End Namespace
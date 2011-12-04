' Copyright 2011, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201109

Imports System

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109
  ''' <summary>
  ''' This code example illustrates how to retrieve all the user lists for
  ''' an account.
  '''
  ''' Tags: UserListService.get
  ''' </summary>
  Class GetAllUserLists
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example illustrates how to retrieve all the user lists for an account."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New GetAllUserLists
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the UserListService.
      Dim userListService As UserListService = user.GetService( _
          AdWordsService.v201109.UserListService)

      ' Create the selector.
      Dim selector As New Selector
      selector.fields = New String() {"Id", "Name", "Status", "Size"}

      Try
        Dim page As UserListPage = userListService.get(selector)
        If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
          For Each userList As UserList In page.entries
            Console.WriteLine("User list name is ""{0}"", id is {1}, status is ""{2}"" and " & _
                "number of users is {3}.", userList.name, userList.id, userList.status, _
                userList.size)
          Next
        Else
          Console.WriteLine("No user lists were found.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to retrieve user lists. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace
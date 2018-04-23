Imports System
Imports System.Linq

Namespace Scaffolding.ManyToMany.Common.DataModel
    ''' <summary>
    ''' The database-independent exception used in Data Layer and View Model Layer to handle database errors.
    ''' </summary>
    Public Class DbException
        Inherits Exception

        ''' <summary>
        ''' Initializes a new instance of the DbRepository class.
        ''' </summary>
        ''' <param name="errorMessage">An error message text.</param>
        ''' <param name="errorCaption">An error message caption text.</param>
        ''' <param name="innerException">An underlying exception.</param>
        Public Sub New(ByVal errorMessage As String, ByVal errorCaption As String, ByVal innerException As Exception)
            MyBase.New(innerException.Message, innerException)
            Me.ErrorMessage = errorMessage
            Me.ErrorCaption = errorCaption
        End Sub

        ''' <summary>The error message text.</summary>
        Private privateErrorMessage As String
        Public Property ErrorMessage() As String
            Get
                Return privateErrorMessage
            End Get
            Private Set(ByVal value As String)
                privateErrorMessage = value
            End Set
        End Property

        ''' <summary>The error message caption text.</summary>
        Private privateErrorCaption As String
        Public Property ErrorCaption() As String
            Get
                Return privateErrorCaption
            End Get
            Private Set(ByVal value As String)
                privateErrorCaption = value
            End Set
        End Property
    End Class
End Namespace
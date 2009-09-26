Imports System
Imports Microsoft.VisualStudio.CommandBars
Imports Extensibility
Imports EnvDTE
Imports EnvDTE80

Public Class Connect

    Implements IDTExtensibility2
    Implements IDTCommandTarget

    Private _applicationObject As DTE2
    Private _addInInstance As AddIn

    '''<summary>Implements the constructor for the Add-in object. Place your initialization code within this method.</summary>
    Public Sub New()

    End Sub

    '''<summary>Implements the OnConnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being loaded.</summary>
    '''<param name='application'>Root object of the host application.</param>
    '''<param name='connectMode'>Describes how the Add-in is being loaded.</param>
    '''<param name='addInInst'>Object representing this Add-in.</param>
    '''<remarks></remarks>
    Public Sub OnConnection(ByVal application As Object _
        , ByVal connectMode As ext_ConnectMode _
        , ByVal addInInst As Object _
        , ByRef custom As Array) Implements IDTExtensibility2.OnConnection

        _applicationObject = CType(application, DTE2)
        _addInInstance = CType(addInInst, AddIn)

        If connectMode = ext_ConnectMode.ext_cm_UISetup Then
            Dim commands As Commands2 = CType(_applicationObject.Commands, Commands2)
            Dim toolsMenuName As String = "Database Project"

            'Place the command on the Database Project context menu.
            Dim commandBars As CommandBars = CType(_applicationObject.CommandBars, CommandBars)
            Dim dbProjCommandBar As CommandBar = commandBars.Item("Database Project")

            'check to see if the command already exists
            Dim myCmd As Command = Nothing
            For Each c As CommandBarControl In dbProjCommandBar.Controls
                If TypeOf c Is Command AndAlso CType(c, Command).Name = "vs_addin" Then
                    myCmd = CType(c, Command)
                    Exit For
                End If
            Next

            If myCmd Is Nothing Then
                'add the command
                Dim command As Command = commands.AddNamedCommand2( _
                    _addInInstance, "vs_addin", "vs_addin", _
                    "Executes the command for vs_addin", True, 59, Nothing, _
                    CType(vsCommandStatus.vsCommandStatusSupported, Integer) _
                    + CType(vsCommandStatus.vsCommandStatusEnabled, Integer), _
                    vsCommandStyle.vsCommandStylePictAndText, _
                    vsCommandControlType.vsCommandControlTypeButton)

                'Find the appropriate command bar on the MenuBar command bar:
                command.AddControl(dbProjCommandBar, 1)
            End If


        End If
    End Sub

    '''<summary>Implements the OnDisconnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being unloaded.</summary>
    '''<param name='disconnectMode'>Describes how the Add-in is being unloaded.</param>
    '''<param name='custom'>Array of parameters that are host application specific.</param>
    '''<remarks></remarks>
    Public Sub OnDisconnection(ByVal disconnectMode As ext_DisconnectMode, ByRef custom As Array) Implements IDTExtensibility2.OnDisconnection
    End Sub

    '''<summary>Implements the OnAddInsUpdate method of the IDTExtensibility2 interface. Receives notification that the collection of Add-ins has changed.</summary>
    '''<param name='custom'>Array of parameters that are host application specific.</param>
    '''<remarks></remarks>
    Public Sub OnAddInsUpdate(ByRef custom As Array) Implements IDTExtensibility2.OnAddInsUpdate
    End Sub

    '''<summary>Implements the OnStartupComplete method of the IDTExtensibility2 interface. Receives notification that the host application has completed loading.</summary>
    '''<param name='custom'>Array of parameters that are host application specific.</param>
    '''<remarks></remarks>
    Public Sub OnStartupComplete(ByRef custom As Array) Implements IDTExtensibility2.OnStartupComplete
        Dim a As String = ""
    End Sub

    '''<summary>Implements the OnBeginShutdown method of the IDTExtensibility2 interface. Receives notification that the host application is being unloaded.</summary>
    '''<param name='custom'>Array of parameters that are host application specific.</param>
    '''<remarks></remarks>
    Public Sub OnBeginShutdown(ByRef custom As Array) Implements IDTExtensibility2.OnBeginShutdown
    End Sub

    '''<summary>Implements the QueryStatus method of the IDTCommandTarget interface. This is called when the command's availability is updated</summary>
    '''<param name='commandName'>The name of the command to determine state for.</param>
    '''<param name='neededText'>Text that is needed for the command.</param>
    '''<param name='status'>The state of the command in the user interface.</param>
    '''<param name='commandText'>Text requested by the neededText parameter.</param>
    '''<remarks></remarks>
    Public Sub QueryStatus(ByVal commandName As String, ByVal neededText As vsCommandStatusTextWanted, ByRef status As vsCommandStatus, ByRef commandText As Object) Implements IDTCommandTarget.QueryStatus
        If neededText = vsCommandStatusTextWanted.vsCommandStatusTextWantedNone Then
            If commandName = "vs_addin.Connect.vs_addin" Then
                status = CType(vsCommandStatus.vsCommandStatusEnabled + vsCommandStatus.vsCommandStatusSupported, vsCommandStatus)
            Else
                status = vsCommandStatus.vsCommandStatusUnsupported
            End If
        End If
    End Sub

    '''<summary>Implements the Exec method of the IDTCommandTarget interface. This is called when the command is invoked.</summary>
    '''<param name='commandName'>The name of the command to execute.</param>
    '''<param name='executeOption'>Describes how the command should be run.</param>
    '''<param name='varIn'>Parameters passed from the caller to the command handler.</param>
    '''<param name='varOut'>Parameters passed from the command handler to the caller.</param>
    '''<param name='handled'>Informs the caller if the command was handled or not.</param>
    '''<remarks></remarks>
    Public Sub Exec(ByVal commandName As String, ByVal executeOption As vsCommandExecOption, ByRef varIn As Object, ByRef varOut As Object, ByRef handled As Boolean) Implements IDTCommandTarget.Exec
        handled = False
        If executeOption = vsCommandExecOption.vsCommandExecOptionDoDefault Then
            If commandName = "vs_addin.Connect.vs_addin" Then
                MessageBox.Show("Hello World")
                handled = True
                Exit Sub
            End If
        End If
    End Sub
End Class

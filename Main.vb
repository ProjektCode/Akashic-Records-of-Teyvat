﻿Imports System.Runtime.InteropServices
'29,19,45 - Light purple
#Region "To-Do List"
'[]Make use of the whtie space in {Farming} by displaying information when hovered over an image.
'[]Resize all artifact images
'[]Resize all character card images
'[]Finish list of character builds
'[]Finish builds layout
'[]Find a better way to combine certain arrays into a "utilities" class
'[]Better way to remove items from the list and center them in the builds panel.
#End Region
Public Class formMain
#Region "Variables"
    Dim farm As Farmables = New Farmables
    Dim bld As Buildables = New Buildables

    Dim Offset As Point = New Point
    Dim panels() As Panel = {conFarming, conBuilds}

    Dim mouseDown_ As Boolean = False
    Dim farmFix As Boolean = False
    Dim isViewing As Boolean = False

#Region "Desc Text" 'Usless variables to amplify laziness
    Dim farmTxt As String = "Lets see what there is to farm today! Oh wait I have no resin left."
    Dim buildTxt As String = "Popular builds for your favorite characters"
    Dim expTxt As String = "Lets see how much pain you'll experience"
    Dim weaTxt As String = "Ahh I see you see the knowledge"
    Dim charTxt As String = "Looking for someone to simp? Simple, Ganyu."
    Dim abTxtx As String = "Information about the author and giving credit where credit is due."
#End Region
#End Region
    Private Sub base_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Region = Region.FromHrgn(CreateRoundRgn(0, 0, Width, Height, 20, 20)) 'Sets form corners to be rounded
        farm.farmables()
        Defaults()
    End Sub
#Region "Events"
    <DllImport("Gdi32.dll", EntryPoint:="CreateRoundRectRgn")>'Creates rounded form
    Private Shared Function CreateRoundRgn(ByVal nLeftRect As Integer, ByVal nTopRect As Integer, ByVal nRightRect As Integer, ByVal nBottomRect As Integer, ByVal nWidthEllipse As Integer, ByVal nHeightEllipse As Integer) As IntPtr
    End Function

#Region "Button Click Events"
    Private Sub btnFarming_Click(sender As Object, e As EventArgs) Handles btnFarming.Click
        onButtonSelected(pnlNav, btnFarming, "FARMING", farmTxt)
        Visibility(conFarming)

    End Sub

    Private Sub btnBuilds_Click(sender As Object, e As EventArgs) Handles btnBuilds.Click
        onButtonSelected(pnlNav, btnBuilds, "BUILDS", buildTxt)
        Visibility(conBuilds)

    End Sub

    Private Sub btnExp_Click(sender As Object, e As EventArgs) Handles btnExp.Click
        onButtonSelected(pnlNav, btnExp, "EXP CALCULATOR", expTxt)
    End Sub

    Private Sub btnWeaponRecords_Click(sender As Object, e As EventArgs) Handles btnWeaponRecords.Click
        onButtonSelected(pnlNav, btnWeaponRecords, "WEAPON RECORDS", weaTxt)
    End Sub

    Private Sub btnCharacterRecords_Click(sender As Object, e As EventArgs) Handles btnCharacterRecords.Click
        onButtonSelected(pnlNav, btnCharacterRecords, "CHARACTER RECORDS", charTxt)
    End Sub

    Private Sub btnCredits_Click(sender As Object, e As EventArgs) Handles btnAbout.Click
        If farmFix = False Then
            fixBtnFarm()
            farmFix = True
        End If
        onButtonSelected(pnlNav, btnAbout, "ABOUT", abTxtx)
    End Sub
#End Region

#Region "Button Leave Events"
    Private Sub btnFarming_Leave(sender As Object, e As EventArgs) Handles btnFarming.Leave
        onButtonLeave(btnFarming)
    End Sub
    Private Sub btnBuilds_Leave(sender As Object, e As EventArgs) Handles btnBuilds.Leave
        onButtonLeave(btnBuilds)
    End Sub
    Private Sub btnExp_Leave(sender As Object, e As EventArgs) Handles btnExp.Leave
        onButtonLeave(btnExp)
    End Sub
    Private Sub btnWeaponRecords_Leave(sender As Object, e As EventArgs) Handles btnWeaponRecords.Leave
        onButtonLeave(btnWeaponRecords)
    End Sub
    Private Sub btnCharacterRecords_Leave(sender As Object, e As EventArgs) Handles btnCharacterRecords.Leave
        onButtonLeave(btnCharacterRecords)
    End Sub
    Private Sub btnCredits_Leave(sender As Object, e As EventArgs) Handles btnAbout.Leave
        onButtonLeave(btnAbout)
    End Sub
#End Region

#Region "Draggable Form"
    Private Sub mouseDown_Event(sender As Object, e As MouseEventArgs) Handles pnlDrag.MouseDown
        Offset.X = e.X
        Offset.Y = e.Y
        mouseDown_ = True
    End Sub

    Private Sub mouseMove_Event(sender As Object, e As MouseEventArgs) Handles pnlDrag.MouseMove
        If mouseDown_ = True Then
            Dim currentPointPos As Point = PointToScreen(e.Location)
            Location = New Point(currentPointPos.X - Offset.X, currentPointPos.Y - Offset.Y)
        End If
    End Sub

    Private Sub mouseUp_Event(sender As Object, e As MouseEventArgs) Handles pnlDrag.MouseUp
        mouseDown_ = False
    End Sub
#End Region

#End Region

#Region "Methods"
    'When a button is selected move Navigation Panel to button and change buttons color
    Private Sub onButtonSelected(pnl As Panel, btn As Button, title As String, desc As String)
        pnl.Height = btn.Height
        pnl.Top = btn.Top
        pnl.Left = btn.Left
        btn.BackColor = Color.FromArgb(29, 19, 45)
        btn.ForeColor = Color.FromArgb(65, 57, 76)
        btn.Font = New Font(btn.Font, FontStyle.Bold)
        lblName.Text = title
        lblDesc.Text = desc

    End Sub

    'When selecting another button revert changes
    Private Sub onButtonLeave(btn As Button)
        If farmFix = False Then
            fixBtnFarm()
            farmFix = True
        End If
        btn.BackColor = Color.FromArgb(11, 7, 17)
        btn.ForeColor = Color.FromArgb(220, 20, 60)
        btn.Font = New Font(btn.Font, FontStyle.Regular)
    End Sub

    'Fix issues the Farm Button has been having until knowing the issue
    Private Sub fixBtnFarm()
        If btnFarming.BackColor = Color.FromArgb(8, 105, 114) Then
        Else
            btnFarming.BackColor = Color.FromArgb(11, 7, 17)
            btnFarming.ForeColor = Color.FromArgb(220, 20, 60)
            btnFarming.Font = New Font(btnFarming.Font, FontStyle.Regular)
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub
    Private Sub btnMinimize_Click(sender As Object, e As EventArgs) Handles btnMinimize.Click
        WindowState = FormWindowState.Minimized
    End Sub

    'When makes the selected panel visible with disabling the others
    Private Sub Visibility(p As Panel)
        If p Is conFarming Then
            conBuilds.Visible = False
            p.Visible = True
        ElseIf p Is conBuilds Then
            conFarming.Visible = False
            p.Visible = True
        End If
    End Sub
    'Default settings for the program to use on load.
    Private Sub Defaults()
        'issues with the about button sometimes loading with a crimson border. this removes it on load.
        btnAbout.FlatAppearance.BorderSize = 0
        btnAbout.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255)
        onButtonSelected(pnlNav, btnFarming, "FARMING", farmTxt)
        conFarming.Visible = True
        conBuilds.Visible = False
        lblAppName.Text = Application.ProductName
    End Sub
#End Region

#Region "Builds"
    Private Sub loadCharacters(i As Image(), text As String)
        bld.ClearPictureBoxes(flowCharacters)
        bld.loadFilteredImages(i)
        With lblElements
            .Text = text
            .Location = New Point(flowCharacters.Width \ 2 - lblElements.Width + 25, lblElements.Location.Y)
        End With
    End Sub
    Private Sub pbElementPyro_Click(sender As Object, e As EventArgs) Handles pbElementPyro.Click
        loadCharacters(bld.pyro, "Pyro")

    End Sub

    Private Sub pbElementCryo_Click(sender As Object, e As EventArgs) Handles pbElementCryo.Click
        loadCharacters(bld.cryo, "Cryo")
    End Sub

    Private Sub pbElementHydro_Click(sender As Object, e As EventArgs) Handles pbElementHydro.Click
        loadCharacters(bld.hydro, "Hydro")
    End Sub

    Private Sub pbElementGeo_Click(sender As Object, e As EventArgs) Handles pbElementGeo.Click
        loadCharacters(bld.geo, "Geo")
    End Sub

    Private Sub pbElementElectro_Click(sender As Object, e As EventArgs) Handles pbElementElectro.Click
        loadCharacters(bld.electro, "Electro")
    End Sub

    Private Sub pbElementAnemo_Click(sender As Object, e As EventArgs) Handles pbElementAnemo.Click
        loadCharacters(bld.anemo, "Anemo")
    End Sub

#End Region

End Class

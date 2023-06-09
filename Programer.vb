﻿Public Class Programmer
    Dim il, fnl As Integer
    Public Shared Function BinaryToASCII(bin As String) As String
        Dim ascii As String = String.Empty

        For i As Integer = 0 To bin.Length - 1 Step 8
            ascii += ChrW(BinaryToDecimal(bin.Substring(i, 8)))
        Next

        Return ascii
    End Function

    Private Shared Function BinaryToDecimal(bin As String) As Integer
        Dim binLength As Integer = bin.Length
        Dim dec As Double = 0

        For i As Integer = 0 To binLength - 1
            dec += (CByte(AscW(bin(i))) - 48) * Math.Pow(2, ((binLength - i) - 1))
        Next

        Return CInt(Math.Truncate(dec))
    End Function

    Public Shared Function HexadecimalToDecimal(hex As String) As Integer
        hex = hex.ToUpper()

        Dim hexLength As Integer = hex.Length
        Dim dec As Double = 0

        For i As Integer = 0 To hexLength - 1
            Dim b As Byte = CByte(AscW(hex(i)))

            If b >= 48 AndAlso b <= 57 Then
                b -= 48
            ElseIf b >= 65 AndAlso b <= 70 Then
                b -= 55
            End If

            dec += b * Math.Pow(16, ((hexLength - i) - 1))
        Next

        Return CInt(Math.Truncate(dec))
    End Function
    Public Shared Function DecimalToHexadecimal(dec As Integer) As String
        If dec < 1 Then Return "0"

        Dim hex As Integer = dec
        Dim hexStr As String = String.Empty

        While dec > 0
            hex = dec Mod 16

            If hex < 10 Then
                hexStr = hexStr.Insert(0, Convert.ToChar(hex + 48).ToString())
            Else
                hexStr = hexStr.Insert(0, Convert.ToChar(hex + 55).ToString())
            End If

            dec = Int(dec / 16)
        End While

        Return hexStr
    End Function
    Private Function OctToBinary(octal As String) As String
        Return Convert.ToString(Convert.ToInt32(octal, 8), 2)
    End Function

    Public Shared Function DecimalToOctal(dec As Integer) As String
        If dec < 1 Then Return "0"

        Dim octStr As String = String.Empty

        While dec > 0
            octStr = octStr.Insert(0, (dec Mod 8).ToString())
            dec = Int(dec / 8)
        End While

        Return octStr
    End Function

    Public Shared Function OctalToDecimal(octal As String) As Integer
        Dim octLength As Integer = octal.Length
        Dim dec As Double = 0

        For i As Integer = 0 To octLength - 1
            dec += (CByte(AscW(octal(i))) - 48) * Math.Pow(8, ((octLength - i) - 1))
        Next

        Return CInt(Math.Truncate(dec))
    End Function

    Public Function ConvertFromBinary(ByVal input As String) As Integer
        Dim ret As Integer = 0
        Dim splitInput As Char() = input.ToCharArray
        Dim modifier As Integer = 1
        For i As Integer = splitInput.Length - 1 To 0 Step -1
            Dim thisChar As Integer = Val(splitInput(i))
            If thisChar = 1 Then
                ret += thisChar * modifier
            End If
            modifier *= 2
        Next

        Return ret
    End Function
    Public Shared Function DecimalToBinary(dec As Integer) As String
        If dec < 1 Then Return "0"

        Dim binStr As String = String.Empty

        While dec > 0
            binStr = binStr.Insert(0, (dec Mod 2).ToString())
            dec = Int(dec / 2)
        End While

        Return binStr
    End Function
    Public Sub New()
        InitializeComponent()
        Me.FormBorderStyle = FormBorderStyle.None
        Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.ResizeRedraw, True)

    End Sub

    Private Const cGrip As Integer = 16
    Private Const cCaption As Integer = 32

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)

    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = &H84 Then
            Dim pos As Point = New Point(m.LParam.ToInt32())
            pos = Me.PointToClient(pos)

            If pos.Y < cCaption Then
                m.Result = CType(2, IntPtr)
                Return
            End If

            If pos.X >= Me.ClientSize.Width - cGrip AndAlso pos.Y >= Me.ClientSize.Height - cGrip Then
                m.Result = CType(17, IntPtr)
                Return
            End If
        End If
        MyBase.WndProc(m)
    End Sub
    Private Sub NormalActualToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NormalActualToolStripMenuItem.Click
        Normal.Show()
        Me.Close()
    End Sub

    Private Sub ScientificToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ScientificToolStripMenuItem.Click
        Scientific.Show()
        Me.Close()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        About.Show()
    End Sub

    Private Sub Closea_Click(sender As Object, e As EventArgs) Handles Closea.Click
        Application.Exit()
    End Sub

    Private Sub Minimizea_Click(sender As Object, e As EventArgs) Handles Minimizea.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub
    Public Shared Function FloatToIEEE754(number As Single) As String
        ' Determine the sign bit
        Dim sign As Integer = 0
        If number < 0 Then
            sign = 1
            number = -number
        End If
        ' Determine the exponent and fraction bits
        Dim exponent As Integer = 0
        Dim fraction As Single = 0

        If number <> 0 Then
            ' Normalize the number to the form 1.m * 2^e
            Do While number < 1 OrElse exponent < -127
                number *= 2
                exponent -= 1
            Loop
            Do While number >= 2 AndAlso exponent < 128
                number /= 2
                exponent += 1
            Loop

            ' Calculate the fraction bits
            fraction = number - 1
            For i As Integer = 1 To 23
                fraction *= 2
                If fraction >= 1 Then
                    fraction -= 1
                    exponent += 1
                End If
            Next
        End If

        ' Convert the sign, exponent, and fraction bits to binary
        Dim signBit As String = Convert.ToString(sign, 2).PadLeft(1, "0"c)
        Dim exponentBits As String = Convert.ToString(exponent + 127, 2).PadLeft(8, "0"c)
        Dim fractionBits As String = Convert.ToString(CInt(fraction * 8388608), 2).PadLeft(23, "0"c)

        ' Concatenate the bits and return the result
        Return signBit & exponentBits & fractionBits
    End Function


    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles ButtCalc.Click
        If (DecToBin.Checked = True) Then
            TextBox2.Text = Convert.ToString(Convert.ToInt32(TextBox1.Text, 10), 2)
            il = 10 'NEEDED VARIABLES FOR COPYING RESULTS
            fnl = 2
        Else
            If (BinToDec.Checked = True) Then
                TextBox2.Text = Convert.ToString(Convert.ToInt32(TextBox1.Text, 2), 10)
                il = 2
                fnl = 10
            Else
                If (OctToDec.Checked = True) Then
                    TextBox2.Text = Convert.ToString(Convert.ToInt32(TextBox1.Text, 8), 10)
                    il = 8
                    fnl = 10
                Else
                    If (DecToOct.Checked = True) Then
                        TextBox2.Text = Convert.ToString(Convert.ToInt32(TextBox1.Text, 10), 8)
                        il = 10
                        fnl = 8
                    Else
                        If (OctToBin.Checked = True) Then
                            TextBox2.Text = Convert.ToString(Convert.ToInt32(TextBox1.Text, 8), 2)
                            il = 8
                            fnl = 2
                        Else
                            If (BinToHex.Checked = True) Then
                                TextBox2.Text = Convert.ToString(Convert.ToInt32(TextBox1.Text, 2), 16)
                                il = 2
                                fnl = 16
                            Else
                                If (DecToHex.Checked = True) Then
                                    TextBox2.Text = Convert.ToString(Convert.ToInt32(TextBox1.Text, 10), 16)
                                    il = 10
                                    fnl = 16
                                Else
                                    If (HexToDec.Checked = True) Then
                                        TextBox2.Text = Convert.ToString(Convert.ToInt32(TextBox1.Text, 16), 10)
                                        il = 16
                                        fnl = 10
                                    Else
                                        If (HexToBin.Checked = True) Then
                                            TextBox2.Text = Convert.ToString(Convert.ToInt32(TextBox1.Text, 16), 2)
                                            il = 16
                                            fnl = 2
                                        Else
                                            If (HexToOct.Checked = True) Then
                                                TextBox2.Text = Convert.ToString(Convert.ToInt32(TextBox1.Text, 16), 8)
                                                il = 16
                                                fnl = 8
                                            Else
                                                If (OctToHex.Checked = True) Then
                                                    TextBox2.Text = Convert.ToString(Convert.ToInt32(TextBox1.Text, 8), 16)
                                                    il = 8
                                                    fnl = 16
                                                Else
                                                    If (BintoOct.Checked = True) Then
                                                        TextBox2.Text = Convert.ToString(Convert.ToInt32(TextBox1.Text, 2), 8)
                                                        il = 2
                                                        fnl = 8
                                                    Else
                                                        If (BinToAsc.Checked = True) Then
                                                            TextBox2.Text = BinaryToASCII(TextBox1.Text)
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If


    End Sub
    Private Sub Butt1_Click(sender As Object, e As EventArgs) Handles Butt1.Click
        TextBox1.Text = TextBox1.Text & “1”
    End Sub

    Private Sub Butt0_Click(sender As Object, e As EventArgs) Handles Butt0.Click
        TextBox1.Text = TextBox1.Text & “0”
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Resetr.Click
        CA1andCA2result.Text = ""
        Pastes.Enabled = False
        Copies.Enabled = False
        il = 0
        fnl = 0
        TextBox1.Text = ""
        TextBox2.Text = ""
        F2.Text = ""
        F1.Text = ""
        ButtonBackspace.Enabled = False
        'DECIMAL'
        DecToBin.Checked = False
        DecToOct.Checked = False
        DecToHex.Checked = False
        'BINARY'
        BintoOct.Checked = False
        BinToDec.Checked = False
        BinToHex.Checked = False
        BinToAsc.Checked = False
        'OCTAL'
        OctToBin.Checked = False
        OctToHex.Checked = False
        OctToDec.Checked = False
        'HEXADECIMAL'
        HexToBin.Checked = False
        HexToOct.Checked = False
        HexToDec.Checked = False
        'INPUT'
        Butt0.Enabled = False
        Butt1.Enabled = False
        Butt2.Enabled = False
        Butt3.Enabled = False
        Butt4.Enabled = False
        Butt5.Enabled = False
        Butt7.Enabled = False
        Butt8.Enabled = False
        Butt6.Enabled = False
        Butt9.Enabled = False
        'HEXADECIMAL LETTERS'
        ButtonA.Enabled = False
        ButtonB.Enabled = False
        ButtonC.Enabled = False
        ButtonD.Enabled = False
        ButtonE.Enabled = False
        ButtonF.Enabled = False
        'INPUT/OUTPUT'
        ButtCalc.Enabled = False
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles BinToDec.CheckedChanged
        CA1andCA2result.Text = ""
        CA1_CA2.Visible = False
        Pastes.Enabled = True
        Copies.Enabled = True
        ButtonBackspace.Enabled = True
        ButtonA.Enabled = False
        ButtonB.Enabled = False
        ButtonC.Enabled = False
        ButtonD.Enabled = False
        ButtonE.Enabled = False
        ButtonF.Enabled = False
        Butt0.Enabled = True
        Butt1.Enabled = True
        Butt2.Enabled = False
        Butt3.Enabled = False
        Butt4.Enabled = False
        Butt5.Enabled = False
        Butt7.Enabled = False
        Butt8.Enabled = False
        Butt6.Enabled = False
        Butt9.Enabled = False
        ButtCalc.Enabled = True
        DecToBin.Checked = False
        OctToDec.Checked = False
        DecToOct.Checked = False
        F1.Text = "BIN"
        F2.Text = "DEC"
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub

    Private Sub Programer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MaximumSize = Screen.FromRectangle(Me.Bounds).WorkingArea.Size
        My.Settings.Reload()
        ' Imposta la lingua iniziale
        Dim linguaSelezionata As String = System.Configuration.ConfigurationManager.AppSettings("Lingua")
        If linguaSelezionata = "English" Then
            Label1.Text = "Programmer Calculator"
            CalculatorToolStripMenuItem.Text = "Calculator"
            FastMenuToolStripMenuItem.Text = "Fast Menu"
            AboutToolStripMenuItem.Text = "About"
            SettingsToolStripMenuItem.Text = "Settings"
            BinToAsc.Text = "Binary -> ASCII"
            BinToDec.Text = "Binary -> Decimal"
            BinToHex.Text = "Binary -> Hexadecimal"
            BintoOct.Text = "Binary -> Octal"
            DecToBin.Text = "Decimal -> Binary"
            DecToHex.Text = "Decimal -> Hexadecimal"
            DecToOct.Text = "Decimal -> Octal"
            OctToBin.Text = "Octal -> Binary"
            OctToDec.Text = "Octal -> Decimal"
            OctToHex.Text = "Octal -> Hexadecimal"
            HexToBin.Text = "Hexadecimal -> Binary"
            HexToDec.Text = "Hexadecimal -> Decimal"
            HexToOct.Text = "Hexadecimal -> Octal"
            Pastes.Text = "PASTE"
            Copies.Text = "COPY"
            info1.Text = "Hexadecimal numbers"
        ElseIf linguaSelezionata = "Italian" Then
            Label1.Text = "Calcolatrice per programmatori"
            CalculatorToolStripMenuItem.Text = "Calcolatrici"
            FastMenuToolStripMenuItem.Text = "Menu Veloce"
            AboutToolStripMenuItem.Text = "Informazioni su"
            SettingsToolStripMenuItem.Text = "Impostazioni"
            BinToAsc.Text = "Binario -> ASCII"
            BinToDec.Text = "Binario -> Decimale"
            BinToHex.Text = "Binario -> Esadecimale"
            BintoOct.Text = "Binario -> Ottale"
            DecToBin.Text = "Decimale -> Binario"
            DecToHex.Text = "Decimale -> Esadecimale"
            DecToOct.Text = "Decimale -> Ottale"
            OctToBin.Text = "Ottale -> Binario"
            OctToDec.Text = "Ottale -> Decimale"
            OctToHex.Text = "Ottale -> Esadecimale"
            HexToBin.Text = "Esadecimale -> Binario"
            HexToDec.Text = "Esadecimale -> Decimale"
            HexToOct.Text = "Esadecimale -> Ottale"
            Pastes.Text = "INCOLLA"
            Copies.Text = "COPIA"
            info1.Text = "Numeri esadecimali"
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles DecToBin.CheckedChanged
        CA1andCA2result.Text = ""
        CA1_CA2.Visible = True
        Pastes.Enabled = True
        Copies.Enabled = True
        ButtonBackspace.Enabled = True
        ButtonA.Enabled = False
        ButtonB.Enabled = False
        ButtonC.Enabled = False
        ButtonD.Enabled = False
        ButtonE.Enabled = False
        ButtonF.Enabled = False
        Butt0.Enabled = True
        Butt1.Enabled = True
        Butt2.Enabled = True
        Butt3.Enabled = True
        Butt4.Enabled = True
        Butt5.Enabled = True
        Butt7.Enabled = True
        Butt8.Enabled = True
        Butt6.Enabled = True
        Butt9.Enabled = True
        ButtCalc.Enabled = True
        TextBox1.Text = ""
        TextBox2.Text = ""
        BinToDec.Checked = False
        OctToDec.Checked = False
        DecToOct.Checked = False
        F1.Text = "DEC"
        F2.Text = "BIN"
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Butt3.Click
        TextBox1.Text = TextBox1.Text & “3”
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Butt4.Click
        TextBox1.Text = TextBox1.Text & “4”
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Butt5.Click
        TextBox1.Text = TextBox1.Text & “5”
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Butt6.Click
        TextBox1.Text = TextBox1.Text & “6”
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Butt7.Click
        TextBox1.Text = TextBox1.Text & “7”
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Butt8.Click
        TextBox1.Text = TextBox1.Text & “8”
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Butt9.Click
        TextBox1.Text = TextBox1.Text & “9”
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        CA1andCA2result.Text = ""
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Butt2.Click
        TextBox1.Text = TextBox1.Text & “2”
    End Sub

    Private Sub DectoOct_CheckedChanged(sender As Object, e As EventArgs) Handles DecToOct.CheckedChanged
        CA1andCA2result.Text = ""
        CA1_CA2.Visible = False
        Pastes.Enabled = True
        Copies.Enabled = True
        ButtonBackspace.Enabled = True
        ButtonA.Enabled = False
        ButtonB.Enabled = False
        ButtonC.Enabled = False
        ButtonD.Enabled = False
        ButtonE.Enabled = False
        ButtonF.Enabled = False
        Butt0.Enabled = True
        Butt1.Enabled = True
        Butt2.Enabled = True
        Butt3.Enabled = True
        Butt4.Enabled = True
        Butt5.Enabled = True
        Butt7.Enabled = True
        Butt8.Enabled = True
        Butt6.Enabled = True
        Butt9.Enabled = True
        ButtCalc.Enabled = True
        TextBox1.Text = ""
        TextBox2.Text = ""
        BinToDec.Checked = False
        DecToBin.Checked = False
        OctToDec.Checked = False
        F1.Text = "DEC"
        F2.Text = "OCT"
    End Sub

    Private Sub OctaltoDec_CheckedChanged(sender As Object, e As EventArgs) Handles OctToDec.CheckedChanged
        CA1andCA2result.Text = ""
        CA1_CA2.Visible = False
        Pastes.Enabled = True
        Copies.Enabled = True
        ButtonBackspace.Enabled = True
        ButtonA.Enabled = False
        ButtonB.Enabled = False
        ButtonC.Enabled = False
        ButtonD.Enabled = False
        ButtonE.Enabled = False
        ButtonF.Enabled = False
        Butt0.Enabled = True
        Butt1.Enabled = True
        Butt2.Enabled = True
        Butt3.Enabled = True
        Butt4.Enabled = True
        Butt5.Enabled = True
        Butt7.Enabled = True
        Butt8.Enabled = False
        Butt6.Enabled = True
        Butt9.Enabled = False
        ButtCalc.Enabled = True
        TextBox1.Text = ""
        TextBox2.Text = ""
        BinToDec.Checked = False
        DecToBin.Checked = False
        DecToOct.Checked = False
        F1.Text = "OCT"
        F2.Text = "DEC"
    End Sub

    Private Sub BinToHex_CheckedChanged(sender As Object, e As EventArgs) Handles BinToHex.CheckedChanged
        CA1andCA2result.Text = ""
        CA1_CA2.Visible = False
        Pastes.Enabled = True
        Copies.Enabled = True
        ButtonBackspace.Enabled = True
        ButtonA.Enabled = False
        ButtonB.Enabled = False
        ButtonC.Enabled = False
        ButtonD.Enabled = False
        ButtonE.Enabled = False
        ButtonF.Enabled = False
        Butt0.Enabled = True
        Butt1.Enabled = True
        Butt2.Enabled = False
        Butt3.Enabled = False
        Butt4.Enabled = False
        Butt5.Enabled = False
        Butt7.Enabled = False
        Butt8.Enabled = False
        Butt6.Enabled = False
        Butt9.Enabled = False
        ButtCalc.Enabled = True
        'ALL CONVERTIONS'
        DecToBin.Checked = False
        OctToDec.Checked = False
        DecToOct.Checked = False
        BinToDec.Checked = False
        BintoOct.Checked = False
        OctToBin.Checked = False
        OctToHex.Checked = False
        DecToHex.Checked = False
        F1.Text = "BIN"
        F2.Text = "HEX"
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub

    Private Sub BintoOct_CheckedChanged(sender As Object, e As EventArgs) Handles BintoOct.CheckedChanged
        CA1andCA2result.Text = ""
        CA1_CA2.Visible = False
        Pastes.Enabled = True
        Copies.Enabled = True
        ButtonBackspace.Enabled = True
        ButtonA.Enabled = False
        ButtonB.Enabled = False
        ButtonC.Enabled = False
        ButtonD.Enabled = False
        ButtonE.Enabled = False
        ButtonF.Enabled = False
        Butt0.Enabled = True
        Butt1.Enabled = True
        Butt2.Enabled = False
        Butt3.Enabled = False
        Butt4.Enabled = False
        Butt5.Enabled = False
        Butt7.Enabled = False
        Butt8.Enabled = False
        Butt6.Enabled = False
        Butt9.Enabled = False
        ButtCalc.Enabled = True
        'ALL CONVERTIONS'
        DecToBin.Checked = False
        OctToDec.Checked = False
        DecToOct.Checked = False
        BinToDec.Checked = False
        BinToHex.Checked = False
        OctToBin.Checked = False
        OctToHex.Checked = False
        DecToHex.Checked = False
        F1.Text = "BIN"
        F2.Text = "OCT"
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub

    Private Sub OctToBin_CheckedChanged(sender As Object, e As EventArgs) Handles OctToBin.CheckedChanged
        CA1andCA2result.Text = ""
        CA1_CA2.Visible = True
        Pastes.Enabled = True
        Copies.Enabled = True
        ButtonBackspace.Enabled = True
        ButtonA.Enabled = False
        ButtonB.Enabled = False
        ButtonC.Enabled = False
        ButtonD.Enabled = False
        ButtonE.Enabled = False
        ButtonF.Enabled = False
        Butt0.Enabled = True
        Butt1.Enabled = True
        Butt2.Enabled = True
        Butt3.Enabled = True
        Butt4.Enabled = True
        Butt5.Enabled = True
        Butt7.Enabled = True
        Butt8.Enabled = False
        Butt6.Enabled = True
        Butt9.Enabled = False
        ButtCalc.Enabled = True
        'ALL CONVERTIONS'
        DecToBin.Checked = False
        OctToDec.Checked = False
        DecToOct.Checked = False
        BinToDec.Checked = False
        BinToHex.Checked = False
        BintoOct.Checked = False
        OctToHex.Checked = False
        DecToHex.Checked = False
        F1.Text = "OCT"
        F2.Text = "BIN"
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub

    Private Sub DecToHex_CheckedChanged(sender As Object, e As EventArgs) Handles DecToHex.CheckedChanged
        CA1andCA2result.Text = ""
        CA1_CA2.Visible = False
        Pastes.Enabled = True
        Copies.Enabled = True
        ButtonBackspace.Enabled = True
        ButtonA.Enabled = False
        ButtonB.Enabled = False
        ButtonC.Enabled = False
        ButtonD.Enabled = False
        ButtonE.Enabled = False
        ButtonF.Enabled = False
        Butt0.Enabled = True
        Butt1.Enabled = True
        Butt2.Enabled = True
        Butt3.Enabled = True
        Butt4.Enabled = True
        Butt5.Enabled = True
        Butt7.Enabled = True
        Butt8.Enabled = False
        Butt6.Enabled = True
        Butt9.Enabled = False
        ButtCalc.Enabled = True
        TextBox1.Text = ""
        TextBox2.Text = ""
        DecToBin.Checked = False
        OctToDec.Checked = False
        DecToOct.Checked = False
        BinToDec.Checked = False
        BintoOct.Checked = False
        OctToBin.Checked = False
        OctToHex.Checked = False
        BinToHex.Checked = False
        F1.Text = "DEC"
        F2.Text = "HEX"
    End Sub

    Private Sub HexToOct_CheckedChanged(sender As Object, e As EventArgs) Handles HexToOct.CheckedChanged
        CA1andCA2result.Text = ""
        CA1_CA2.Visible = False
        Pastes.Enabled = True
        Copies.Enabled = True
        ButtonBackspace.Enabled = True
        ButtonA.Enabled = True
        ButtonB.Enabled = True
        ButtonC.Enabled = True
        ButtonD.Enabled = True
        ButtonE.Enabled = True
        ButtonF.Enabled = True
        Butt0.Enabled = True
        Butt1.Enabled = True
        Butt2.Enabled = True
        Butt3.Enabled = True
        Butt4.Enabled = True
        Butt5.Enabled = True
        Butt7.Enabled = True
        Butt8.Enabled = True
        Butt6.Enabled = True
        Butt9.Enabled = True
        ButtCalc.Enabled = True
        TextBox1.Text = ""
        TextBox2.Text = ""
        DecToBin.Checked = False
        OctToDec.Checked = False
        DecToOct.Checked = False
        BinToDec.Checked = False
        BintoOct.Checked = False
        OctToBin.Checked = False
        OctToHex.Checked = False
        BinToHex.Checked = False
        HexToDec.Checked = False
        HexToBin.Checked = False
        F1.Text = "HEX"
        F2.Text = "OCT"
    End Sub

    Private Sub HexToDec_CheckedChanged(sender As Object, e As EventArgs) Handles HexToDec.CheckedChanged
        CA1andCA2result.Text = ""
        CA1_CA2.Visible = False
        Pastes.Enabled = True
        Copies.Enabled = True
        ButtonBackspace.Enabled = True
        ButtonA.Enabled = True
        ButtonB.Enabled = True
        ButtonC.Enabled = True
        ButtonD.Enabled = True
        ButtonE.Enabled = True
        ButtonF.Enabled = True
        Butt0.Enabled = True
        Butt1.Enabled = True
        Butt2.Enabled = True
        Butt3.Enabled = True
        Butt4.Enabled = True
        Butt5.Enabled = True
        Butt7.Enabled = True
        Butt8.Enabled = True
        Butt6.Enabled = True
        Butt9.Enabled = True
        ButtCalc.Enabled = True
        TextBox1.Text = ""
        TextBox2.Text = ""
        DecToBin.Checked = False
        OctToDec.Checked = False
        DecToOct.Checked = False
        BinToDec.Checked = False
        BintoOct.Checked = False
        OctToBin.Checked = False
        OctToHex.Checked = False
        BinToHex.Checked = False
        F1.Text = "HEX"
        F2.Text = "DEC"
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles ButtonA.Click
        TextBox1.Text = TextBox1.Text & "A"
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles ButtonB.Click
        TextBox1.Text = TextBox1.Text & "B"
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles ButtonC.Click
        TextBox1.Text = TextBox1.Text & "C"
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles ButtonD.Click
        TextBox1.Text = TextBox1.Text & "D"
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles ButtonE.Click
        TextBox1.Text = TextBox1.Text & "E"
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles ButtonF.Click
        TextBox1.Text = TextBox1.Text & "F"
    End Sub

    Private Sub HexToBin_CheckedChanged(sender As Object, e As EventArgs) Handles HexToBin.CheckedChanged
        CA1andCA2result.Text = ""
        CA1_CA2.Visible = True
        Pastes.Enabled = True
        Copies.Enabled = True
        ButtonBackspace.Enabled = True
        ButtonA.Enabled = True
        ButtonB.Enabled = True
        ButtonC.Enabled = True
        ButtonD.Enabled = True
        ButtonE.Enabled = True
        ButtonF.Enabled = True
        Butt0.Enabled = True
        Butt1.Enabled = True
        Butt2.Enabled = True
        Butt3.Enabled = True
        Butt4.Enabled = True
        Butt5.Enabled = True
        Butt7.Enabled = True
        Butt8.Enabled = True
        Butt6.Enabled = True
        Butt9.Enabled = True
        ButtCalc.Enabled = True
        TextBox1.Text = ""
        TextBox2.Text = ""
        DecToBin.Checked = False
        OctToDec.Checked = False
        DecToOct.Checked = False
        BinToDec.Checked = False
        BintoOct.Checked = False
        OctToBin.Checked = False
        OctToHex.Checked = False
        BinToHex.Checked = False
        HexToDec.Checked = False
        HexToOct.Checked = False
        F1.Text = "HEX"
        F2.Text = "BIN"
    End Sub

    Private Sub OctToHex_CheckedChanged(sender As Object, e As EventArgs) Handles OctToHex.CheckedChanged
        CA1andCA2result.Text = ""
        CA1_CA2.Visible = False
        Pastes.Enabled = True
        Copies.Enabled = True
        ButtonBackspace.Enabled = True
        ButtonA.Enabled = False
        ButtonB.Enabled = False
        ButtonC.Enabled = False
        ButtonD.Enabled = False
        ButtonE.Enabled = False
        ButtonF.Enabled = False
        Butt0.Enabled = True
        Butt1.Enabled = True
        Butt2.Enabled = True
        Butt3.Enabled = True
        Butt4.Enabled = True
        Butt5.Enabled = True
        Butt7.Enabled = True
        Butt8.Enabled = False
        Butt6.Enabled = True
        Butt9.Enabled = False
        ButtCalc.Enabled = True
        'ALL CONVERTIONS'
        DecToBin.Checked = False
        OctToDec.Checked = False
        DecToOct.Checked = False
        BinToDec.Checked = False
        BinToHex.Checked = False
        BintoOct.Checked = False
        OctToBin.Checked = False
        DecToHex.Checked = False
        HexToDec.Checked = False
        HexToOct.Checked = False
        F1.Text = "OCT"
        F2.Text = "HEX"
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub

    Private Sub ButtonComa_Click(sender As Object, e As EventArgs)
        TextBox1.Text = TextBox1.Text & "."
    End Sub

    Private Sub CheckBox1_CheckedChanged_1(sender As Object, e As EventArgs) Handles BinToAsc.CheckedChanged
        CA1andCA2result.Text = ""
        CA1_CA2.Visible = False
        Pastes.Enabled = True
        Copies.Enabled = True
        ButtonBackspace.Enabled = True
        ButtonA.Enabled = False
        ButtonB.Enabled = False
        ButtonC.Enabled = False
        ButtonD.Enabled = False
        ButtonE.Enabled = False
        ButtonF.Enabled = False
        Butt0.Enabled = True
        Butt1.Enabled = True
        Butt2.Enabled = False
        Butt3.Enabled = False
        Butt4.Enabled = False
        Butt5.Enabled = False
        Butt7.Enabled = False
        Butt8.Enabled = False
        Butt6.Enabled = False
        Butt9.Enabled = False
        ButtCalc.Enabled = True
        'ALL CONVERTIONS'
        DecToBin.Checked = False
        OctToDec.Checked = False
        DecToOct.Checked = False
        BinToDec.Checked = False
        BinToHex.Checked = False
        OctToBin.Checked = False
        OctToHex.Checked = False
        DecToHex.Checked = False
        F1.Text = "BIN"
        F2.Text = "ASCII"
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub

    Private Sub ButtonBackspace_Click(sender As Object, e As EventArgs) Handles ButtonBackspace.Click
        CA1andCA2result.Text = ""
        If TextBox1.Text < " " Then
            TextBox1.Text = Mid(TextBox1.Text, 1, Len(TextBox1.Text) - 1 + 1)
        Else
            TextBox1.Text = Mid(TextBox1.Text, 1, Len(TextBox1.Text) - 1)
        End If
    End Sub
    Private Sub Copies_Click(sender As Object, e As EventArgs) Handles Copies.Click
        My.Computer.Clipboard.SetText("(" & TextBox1.Text & ")" & il & " -> (" & TextBox2.Text & ")" & fnl)
        MsgBox("Copied with success!")
    End Sub

    Private Sub BinaryAdditionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BinaryAdditionToolStripMenuItem.Click
        Me.Close()
        BinaryOp.Show()
    End Sub

    Private Sub FastMenuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FastMenuToolStripMenuItem.Click
        FastMenu.Show()
    End Sub

    Private Sub CA1_CA2_Click(sender As Object, e As EventArgs) Handles CA1_CA2.Click
        Dim binario As String
        Dim complemento1 As String
        Dim complemento2 As String
        binario = TextBox2.Text

        'Conversione in Complemento a 1
        For i As Integer = 0 To binario.Length - 1
            If binario(i) = "0" Then
                complemento1 = complemento1 & "1"
            Else
                complemento1 = complemento1 & "0"
            End If
        Next

        'Conversione in Complemento a 2
        Dim overflow As Integer = 0
        For i As Integer = binario.Length - 1 To 0 Step -1
            If binario(i) = "0" Then
                If overflow = 0 Then
                    complemento2 = complemento2 & "0"
                Else
                    complemento2 = complemento2 & "1"
                End If
            Else
                If overflow = 0 Then
                    complemento2 = complemento2 & "1"
                    overflow = 1
                Else
                    complemento2 = complemento2 & "0"
                End If
            End If
        Next

        CA1andCA2result.Text = "CA2: " & complemento2 & vbCrLf & "CA1: " & complemento1
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles ButtonMinus.Click
        TextBox1.Text = TextBox1.Text & "-"
    End Sub

    Private Sub Button1_Click_2(sender As Object, e As EventArgs) Handles Button1.Click
        FloatingPoint1.show()
    End Sub

    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
        Settingsc.Show()
    End Sub

    Private Sub Pastes_Click(sender As Object, e As EventArgs) Handles Pastes.Click
        TextBox1.Text = Clipboard.GetText()
    End Sub
End Class
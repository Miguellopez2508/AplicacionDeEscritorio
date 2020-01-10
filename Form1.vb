Imports System.Security.Cryptography
Imports System.Text
Imports MySql.Data.MySqlClient
Public Class Form1
    Dim contrasenabase As String
    Dim tipo As String

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        TextBox2.PasswordChar = "*"
    End Sub


    Public Function conectar() As MySqlConnection
        Try
            Dim conexion As New MySqlConnectionStringBuilder()
            conexion.Server = "localhost"
            conexion.UserID = "root"
            conexion.Password = ""
            conexion.Database = "alojamiento"

            Dim con As New MySqlConnection(conexion.ToString())
            con.Open()
            Return con
        Catch ex As Exception
            MsgBox("No se pudo conectar con la base de datos")
            Return Nothing
        End Try
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        Dim connection As MySqlConnection
        connection = conectar()
        Dim consulta As String
        consulta = "select password,tipo from usuario where email = '" & TextBox1.Text & "'"
        Dim comando As New MySqlCommand(consulta)
        comando.Connection = connection
        Dim resultado As MySqlDataReader
        resultado = comando.ExecuteReader
        While resultado.Read()
            contrasenabase = resultado(0)
            tipo = resultado(1)
        End While

        Dim hash As String = getMd5Hash(TextBox2.Text)

        Try
            If tipo = 0 Then
                If contrasenabase.Equals(hash) Then
                    Dim f2 As New Form2
                    f2.Show()
                    Me.Hide()
                Else
                    Label4.Text = "Contraseña incorrecta"
                    Me.TextBox1.Text = ""
                    Me.TextBox2.Text = ""
                End If
            Else
                Label4.Text = "Este usuario no es admin"
            End If
        Catch
            Label4.Text = "Usuario incorrecto"
        End Try

    End Sub

    Function getMd5Hash(ByVal input As String) As String

        Dim md5Hasher As MD5 = MD5.Create()

        Dim data As Byte() = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input))

        Dim sBuilder As New StringBuilder()

        Dim i As Integer
        For i = 0 To data.Length - 1
            sBuilder.Append(data(i).ToString("x2"))
        Next i

        Return sBuilder.ToString()

    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim f2 As New Form2
        f2.Show()
        Me.Hide()
    End Sub
End Class

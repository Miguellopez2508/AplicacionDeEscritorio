Imports MySql.Data.MySqlClient

Public Class GestionUsuarios

    Dim conexion As MySqlConnection = New MySqlConnection("server=localhost;user=root;password=;database=alojamiento")
    Dim adaptador As MySqlDataAdapter
    Dim datos As DataSet
    Dim dni As String

    Private Sub GestionUsuarios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            conexion.Open()
            Dim consulta As String
            consulta = "select * from usuario"

            rellenarDataGrid(consulta, conexion)

            conexion.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            conexion.Close()
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        TextBox1.Text = DataGridView1.Rows(e.RowIndex).Cells("DNI").Value.ToString
        TextBox2.Text = DataGridView1.Rows(e.RowIndex).Cells("NOMBRE").Value.ToString
        TextBox3.Text = DataGridView1.Rows(e.RowIndex).Cells("APELLIDOS").Value.ToString
        TextBox4.Text = DataGridView1.Rows(e.RowIndex).Cells("EMAIL").Value.ToString
        TextBox5.Text = DataGridView1.Rows(e.RowIndex).Cells("TELEFONO").Value.ToString
        TextBox7.Text = DataGridView1.Rows(e.RowIndex).Cells("PASSWORD").Value.ToString

        If DataGridView1.Rows(e.RowIndex).Cells("TIPO").Value.ToString.Equals("True") Then
            CheckBox1.Checked = True
        Else
            CheckBox1.Checked = False
        End If

        dni = TextBox1.Text.ToString
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            conexion.Open()
            Dim consulta As String
            consulta = "select * from usuario where dni = '" & TextBox1.Text.ToString & "'"

            rellenarDataGrid(consulta, conexion)

            conexion.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            conexion.Close()
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            conexion.Open()
            Dim consulta As String
            Dim actualiza As New MySqlCommand("update usuario set dni = @dni, nombre = @nombre, apellidos = @apellidos, email = @email, telefono = @telefono, password = md5(@password), tipo = @tipo where dni = @dni2", conexion)

            actualiza.Parameters.AddWithValue("@dni", TextBox1.Text.ToString)
            actualiza.Parameters.AddWithValue("@nombre", TextBox2.Text.ToString)
            actualiza.Parameters.AddWithValue("@apellidos", TextBox3.Text.ToString)
            actualiza.Parameters.AddWithValue("@email", TextBox4.Text.ToString)
            actualiza.Parameters.AddWithValue("@password", TextBox7.Text.ToString)
            actualiza.Parameters.AddWithValue("@tipo", CheckBox1.Checked)
            actualiza.Parameters.AddWithValue("@dni2", dni)

            Try
                actualiza.Parameters.AddWithValue("@telefono", CInt(TextBox5.Text))
            Catch ex As Exception
                actualiza.Parameters.AddWithValue("@telefono", -1)
            End Try

            actualiza.ExecuteNonQuery()
            consulta = "select * from usuario where dni = '" & TextBox1.Text.ToString & "'"

            rellenarDataGrid(consulta, conexion)

            conexion.Close()
            MsgBox("Actualizado")
        Catch ex As Exception
            MsgBox(ex.Message)
            conexion.Close()
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            conexion.Open()
            Dim consulta As String
            Dim actualiza As New MySqlCommand("delete from usuario where dni ='" & TextBox1.Text.ToString & "'", conexion)
            actualiza.ExecuteNonQuery()
            consulta = "select * from usuario"

            rellenarDataGrid(consulta, conexion)

            conexion.Close()
            MsgBox("Eliminado")
        Catch ex As Exception
            MsgBox(ex.Message)
            conexion.Close()
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            conexion.Open()
            Dim consulta As String
            Dim actualiza As New MySqlCommand("insert into usuario (dni, nombre, apellidos, email, telefono, password, tipo) values('" & TextBox1.Text.ToString & "', '" & TextBox2.Text.ToString & "', '" & TextBox3.Text.ToString & "', '" & TextBox4.Text.ToString & "', " & CInt(TextBox5.Text) & ", md5('" & TextBox7.Text & "'), " & Convert.ToInt32(CheckBox1.Checked) & ")", conexion)
            actualiza.ExecuteNonQuery()
            consulta = "select * from usuario"

            rellenarDataGrid(consulta, conexion)

            conexion.Close()
            MsgBox("Agregado")
        Catch ex As Exception
            MsgBox(ex.Message)
            conexion.Close()
        End Try
    End Sub

    Sub rellenarDataGrid(consulta As String, conexion As MySqlConnection)
        adaptador = New MySqlDataAdapter(consulta, conexion)
        datos = New DataSet
        adaptador.Fill(datos, "usuario")
        DataGridView1.DataSource = datos
        DataGridView1.DataMember = "usuario"
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            conexion.Open()
            Dim consulta As String
            consulta = "select * from usuario"

            rellenarDataGrid(consulta, conexion)

            conexion.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            conexion.Close()
        End Try
    End Sub
End Class
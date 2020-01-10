
Imports System.Globalization
Imports MySql.Data.MySqlClient

Public Class Form2

    Dim con As MySqlConnection = New MySqlConnection("server=localhost;user=root;password=;database=alojamiento")
    Dim datos As DataSet
    Dim datos2 As DataTable
    Dim datos3 As DataTable
    Dim datos4 As DataTable
    Dim adaptador As New MySqlDataAdapter

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        actualizar()
        mostrarDNIs()
        mostrarID_ALOJAMIENTOS()
        ComboBox1.SelectedIndex = -1
        ComboBox2.SelectedIndex = -1
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            Label7.Text = DataGridView1.Rows(e.RowIndex).Cells("ID").Value.ToString
            ComboBox1.Text = DataGridView1.Rows(e.RowIndex).Cells("DNI").Value.ToString
            TextBox2.Text = Format(DateTime.Parse(DataGridView1.Rows(e.RowIndex).Cells("FECHA_INICIO").Value.ToString()), "yyyy/MM/dd")
            TextBox3.Text = Format(DateTime.Parse(DataGridView1.Rows(e.RowIndex).Cells("FECHA_FIN").Value.ToString()), "yyyy/MM/dd")
            ComboBox2.Text = DataGridView1.Rows(e.RowIndex).Cells("ID_ALOJAMIENTO").Value.ToString
        Catch
            MsgBox("Haz click en la fila")
        End Try


    End Sub

    Private Sub Button_BuscarPorDni(sender As Object, e As EventArgs) Handles Button4.Click

        Dim dni As String()
        dni = ComboBox1.Text.Split("(")

        con.Open()
        adaptador = New MySqlDataAdapter("select * from reservas where dni='" & dni(0) & "'", con)
        datos = New DataSet
        adaptador.Fill(datos, "reservas")
        DataGridView1.DataSource = datos
        DataGridView1.DataMember = "reservas"
        con.Close()

    End Sub

    Private Sub Button_Modificar(sender As Object, e As EventArgs) Handles Button1.Click

        Try
            If CDate(TextBox2.Text) > CDate(TextBox3.Text) Then
                MsgBox("Error en las fecha")
            Else
                Dim resultado = MsgBox("¿Estas seguro de modificar esta reserva?", vbOKCancel, "CONFIRMACION")
                If resultado = vbOK Then

                    Dim dni As String()
                    Dim id_alojamiento As String()
                    dni = ComboBox1.Text.Split("(")
                    id_alojamiento = ComboBox2.Text.Split("(")

                    con.Open()
                    Dim query As String = "update  reservas set ID ='" & Label7.Text & "' , DNI='" & dni(0) & "' , FECHA_INICIO='" & TextBox2.Text & "' , FECHA_FIN='" & TextBox3.Text & "' , ID_ALOJAMIENTO='" & id_alojamiento(0) & "' where ID='" & Label7.Text & "'"
                    Dim cambio As New MySqlCommand(query, con)
                    cambio.ExecuteNonQuery()
                    MessageBox.Show("Reserva actualizada correctamente")
                    con.Close()
                    actualizar()
                Else
                    MsgBox("MODIFICACION CANCELADA")
                End If
            End If
        Catch
            MsgBox("ERROR DATOS INCORRECTOS")
        End Try

    End Sub

    Sub actualizar()

        con.Open()
        adaptador = New MySqlDataAdapter("select * from reservas", con)
        datos = New DataSet
        adaptador.Fill(datos, "reservas")
        DataGridView1.DataSource = datos
        DataGridView1.DataMember = "reservas"
        con.Close()

    End Sub

    Private Sub Button_Eliminar(sender As Object, e As EventArgs) Handles Button2.Click

        Try
            Dim resultado = MsgBox("¿Estas seguro de borrar esta reserva?", vbOKCancel, "CONFIRMACION")

            If resultado = vbOK Then

                con.Open()
                Dim query As String = "delete from reservas where ID =" & Label7.Text
                Dim cambio As New MySqlCommand(query, con)
                cambio.ExecuteNonQuery()
                MessageBox.Show("Reserva eliminada correctamente")
                con.Close()

                actualizar()
            Else
                MsgBox("ELIMINACION CANCELADA")
            End If
        Catch
            MsgBox("ERROR DATOS INCORRECTOS")
        End Try


    End Sub

    Private Sub Button_Agregar(sender As Object, e As EventArgs) Handles Button3.Click

        Try

            If CDate(TextBox2.Text) > CDate(TextBox3.Text) Then
                MsgBox("Error en las fecha")
            Else
                Dim resultado = MsgBox("¿Estas seguro de insertar esta reserva?", vbOKCancel, "CONFIRMACION")

                If resultado = vbOK Then

                    Dim dni As String()
                    Dim id_alojamiento As String()
                    dni = ComboBox1.Text.Split("(")
                    id_alojamiento = ComboBox2.Text.Split("(")

                    con.Open()
                    Dim query As String = "insert into reservas (DNI,FECHA_INICIO,FECHA_FIN,ID_ALOJAMIENTO) values ('" & dni(0) & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & id_alojamiento(0) & "')"
                    Dim cambio As New MySqlCommand(query, con)
                    cambio.ExecuteNonQuery()
                    MessageBox.Show("Reserva insertada correctamente")
                    con.Close()
                    actualizar()

                Else
                    MsgBox("INSERCION CANCELADA")
                End If
            End If
        Catch
            MsgBox("ERROR DATOS INCORRECTOS")
        End Try


    End Sub

    Sub mostrarDNIs()

        con.Open()
        adaptador = New MySqlDataAdapter("select DNI from usuario", con)
        datos2 = New DataTable
        adaptador.Fill(datos2)

        adaptador = New MySqlDataAdapter("select NOMBRE from usuario", con)
        datos3 = New DataTable
        adaptador.Fill(datos3)

        adaptador = New MySqlDataAdapter("select APELLIDOS from usuario", con)
        datos4 = New DataTable
        adaptador.Fill(datos4)

        Dim comboSource As New Dictionary(Of String, String)()

        For i = 0 To datos2.Rows.Count - 1

            comboSource.Add(datos2.Rows(i).Item(0), datos2.Rows(i).Item(0) & "(" & datos3.Rows(i).Item(0) & " " & datos4.Rows(i).Item(0) & ")")

        Next

        ComboBox1.DataSource = New BindingSource(comboSource, Nothing)
        ComboBox1.DisplayMember = "Value"
        ComboBox1.ValueMember = "Key"

        con.Close()

    End Sub

    Sub mostrarID_ALOJAMIENTOS()

        con.Open()
        adaptador = New MySqlDataAdapter("select ID from alojamientos", con)
        datos2 = New DataTable
        adaptador.Fill(datos2)

        adaptador = New MySqlDataAdapter("select NOMBRE from alojamientos", con)
        datos3 = New DataTable
        adaptador.Fill(datos3)

        Dim comboSource As New Dictionary(Of String, String)()

        For i = 0 To datos2.Rows.Count - 1
            comboSource.Add(datos2.Rows(i).Item(0), datos2.Rows(i).Item(0) & "(" & datos3.Rows(i).Item(0) & ")")
        Next

        ComboBox2.DataSource = New BindingSource(comboSource, Nothing)
        ComboBox2.DisplayMember = "Value"
        ComboBox2.ValueMember = "Key"
        con.Close()

    End Sub

    Private Sub Button_Volver(sender As Object, e As EventArgs) Handles Button5.Click
        Dim f1 As New Form1
        f1.Show()
        Me.Hide()
    End Sub

    Private Sub Button_VerTodasLasReservas(sender As Object, e As EventArgs) Handles Button6.Click
        actualizar()
    End Sub


End Class

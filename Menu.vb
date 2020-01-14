Public Class Menu
    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim f1 As New GestionUsuarios
        f1.Show()
        Me.Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim f1 As New GestionReservas
        f1.Show()
        Me.Hide()
    End Sub
End Class
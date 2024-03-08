public class Medicina : Medicinas
{
    public string Texto {  get; set; }
    public TipoMedicina TipoMedicina { get; set; }
    public Medicina() { }
    public Medicina(string id, string nombre, int cantidad, string descripcion, TipoMedicina tipoMedicina, string texto) : base(id, nombre, cantidad, descripcion)
    {
        Texto = texto;
        TipoMedicina = tipoMedicina;
    }
}
public enum TipoMedicina
{
    Estado,
    Vida
}

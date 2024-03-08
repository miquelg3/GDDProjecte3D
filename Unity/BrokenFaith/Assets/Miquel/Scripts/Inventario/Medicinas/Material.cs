public class Material : Medicinas
{
    public TipoMaterial TipoMaterial { get; set; }
    public Material() { }
    public Material(string id, string nombre, int cantidad, string descripcion, TipoMaterial tipoMaterial) : base(id, nombre, cantidad, descripcion)
    {
        TipoMaterial = tipoMaterial;
    }
}
public enum TipoMaterial
{
    Quirurgico,
    Hierba,
    Polvo
}

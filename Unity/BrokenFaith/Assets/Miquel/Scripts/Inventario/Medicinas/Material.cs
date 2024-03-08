public class Material : Medicinas
{
    public TipoMaterial TipoMaterial { get; set; }
    public Material() { }
    public Material(string id, string nombre, int cantidad, float escala, string descripcion, TipoMaterial tipoMaterial) : base(id, nombre, descripcion, escala, cantidad)
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

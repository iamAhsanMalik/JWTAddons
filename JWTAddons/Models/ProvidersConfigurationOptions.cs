namespace JWTAddons.Models;

public class ProvidersConfigurationOptions
{
    public Google Google { get; set; } = new Google();
    public Facebook Facebook { get; set; } = new Facebook();
    public Apple Apple { get; set; } = new Apple();
}

public class Google
{
    public string? ISS { get; set; }
    public string? AUD { get; set; }
    public string? SUB { get; set; }
    public string? KID { get; set; }
    public string? PrivateKey { get; set; }
    public string? ValidationKey { get; set; }
    public string? PublicKey { get; set; }
}
public class Facebook
{
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
}
public class Apple
{
    public string? ISS { get; set; }
    public string? AUD { get; set; }
    public string? SUB { get; set; }
    public string? KID { get; set; }
    public string? PrivateKey { get; set; }
    public string? ValidationKey { get; set; }
    public string? PublicKey { get; set; }

}

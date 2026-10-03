using System;
using System.IO;
using Xunit;

public class DesenharTest
{
    private string CapturarDesenho(Forma forma)
    {
        TextWriter saidaOriginal = Console.Out;
        StringWriter saida = new StringWriter();

        Console.SetOut(saida);
        try
        {
            forma.Desenhar();
        }
        finally
        {
            Console.SetOut(saidaOriginal);
        }

        return saida.ToString().TrimEnd('\r', '\n');
    }

    [Fact]
    public void Desenhar_Linha_EscreveSuaString()
    {
        Linha linha = new Linha(10, 90, 250, 90, "255,0,0", 5);

        Assert.Equal("1;10;90;250;90;255,0,0;5", CapturarDesenho(linha));
    }

    [Fact]
    public void Desenhar_Retangulo_EscreveSuaString()
    {
        Retangulo retangulo = new Retangulo(80, 40, 120, 60, "0,255,0");

        Assert.Equal("2;80;40;120;60;0,255,0", CapturarDesenho(retangulo));
    }

    [Fact]
    public void Desenhar_Circulo_EscreveSuaString()
    {
        Circulo circulo = new Circulo(200, 150, 45, "255,128,0");

        Assert.Equal("3;200;150;45;255,128,0", CapturarDesenho(circulo));
    }

    [Fact]
    public void Desenhar_Elipse_EscreveSuaString()
    {
        Elipse elipse = new Elipse(150, 120, 80, 35, "255,255,255");

        Assert.Equal("4;150;120;80;35;255,255,255", CapturarDesenho(elipse));
    }

    [Fact]
    public void Desenhar_Poligono_EscreveSuaString()
    {
        Poligono poligono = new Poligono(
            new System.Collections.Generic.List<int> { 10, 10, 110, 10, 60, 90 },
            "0,0,255");

        Assert.Equal("5;10;10;110;10;60;90;0,0,255", CapturarDesenho(poligono));
    }

    [Fact]
    public void Desenhar_Texto_EscreveSuaString()
    {
        TextoForma texto = new TextoForma(30, 200, "Bom Dia", "0,0,0");

        Assert.Equal("6;30;200;Bom Dia;0,0,0", CapturarDesenho(texto));
    }

    [Fact]
    public void Desenhar_ImagemVetorial_EscreveTodasAsFormas()
    {
        ImagemVetorial imagem = new ImagemVetorial(600, 400);
        imagem.Formas.Add(new Circulo(520, 80, 40, "255,255,0"));

        TextWriter saidaOriginal = Console.Out;
        StringWriter saida = new StringWriter();

        Console.SetOut(saida);
        try
        {
            imagem.Desenhar();
        }
        finally
        {
            Console.SetOut(saidaOriginal);
        }

        string resultado = saida.ToString().TrimEnd('\r', '\n');
        Assert.Equal("0;600;400\n3;520;80;40;255,255,0", resultado.Replace("\r\n", "\n"));
    }
}

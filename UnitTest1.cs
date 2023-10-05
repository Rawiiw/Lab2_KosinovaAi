using System;
using Xunit;
using Serilog;

public class TriangleTests
{
    public TriangleTests()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }

    ~TriangleTests()
    {
        Log.CloseAndFlush();
    }

    [Fact]
    public void TestValidTriangle()
    {
        Triangle tri = new Triangle();
        var result = tri.OutputCheck("3", "4", "5");
        Assert.Equal("Разносторонний", result.Item1);
        Assert.Equal(3, result.Item2[0].Item1);
        Assert.Equal(100, result.Item2[0].Item2);
    }

    [Fact]
    public void TestEquilateralTriangle()
    {
        Triangle tri = new Triangle();
        var result = tri.OutputCheck("5", "5", "5");
        Assert.Equal("Равносторонний", result.Item1);
        Assert.Equal(0, result.Item2[0].Item1);
        Assert.Equal(100, result.Item2[0].Item2);
    }

    [Fact]
    public void TestIsoscelesTriangle()
    {
        Triangle tri = new Triangle();
        var result = tri.OutputCheck("2", "2", "3");
        Assert.Equal("Равнобедренный", result.Item1);
        Assert.Equal(0, result.Item2[0].Item1);
        Assert.Equal(100, result.Item2[0].Item2);
    }

    [Fact]
    public void TestInvalidTriangle()
    {
        Triangle tri = new Triangle();
        var result = tri.OutputCheck("1", "2", "5");
        Assert.Equal("Не треугольник", result.Item1);
        Assert.Equal(-1, result.Item2[0].Item1);
        Assert.Equal(-1, result.Item2[0].Item2);
    }

    [Fact]
    public void TestNonNumericInput()
    {
        Triangle tri = new Triangle();
        var result = tri.OutputCheck("1w", "2e", "5");
        Assert.Equal("", result.Item1);
        Assert.Equal(-2, result.Item2[0].Item1);
        Assert.Equal(-2, result.Item2[0].Item2);
    }

    [Fact]
    public void TestNegativeSide()
    {
        Triangle tri = new Triangle();
        var result = tri.OutputCheck("-1", "2", "5");
        Assert.Equal("Не треугольник", result.Item1);
        Assert.Equal(-1, result.Item2[0].Item1);
        Assert.Equal(-1, result.Item2[0].Item2);
    }

    [Fact]
    public void TestZeroSide()
    {
        Triangle tri = new Triangle();
        var result = tri.OutputCheck("0", "2", "5");
        Assert.Equal("Не треугольник", result.Item1);
        Assert.Equal(-1, result.Item2[0].Item1);
        Assert.Equal(-1, result.Item2[0].Item2);
    }

    [Fact]
    public void TestNegativeAndZeroSides()
    {
        Triangle tri = new Triangle();
        var result = tri.OutputCheck("-1", "0", "-5");
        Assert.Equal("Не треугольник", result.Item1);
        Assert.Equal(-1, result.Item2[0].Item1);
        Assert.Equal(-1, result.Item2[0].Item2);
    }

    [Fact]
    public void TestEmptyInput()
    {
        Triangle tri = new Triangle();
        var result = tri.OutputCheck("", "2", "5");
        Assert.Equal("", result.Item1);
        Assert.Equal(-2, result.Item2[0].Item1);
        Assert.Equal(-2, result.Item2[0].Item2);
    }

    [Fact]
    public void TestMissingSide()
    {
        Triangle tri = new Triangle();
        var result = tri.OutputCheck("3", "", "5");
        Assert.Equal("", result.Item1);
        Assert.Equal(-2, result.Item2[0].Item1);
        Assert.Equal(-2, result.Item2[0].Item2);
    }

    [Fact]
    public void TestValidFloatingPointSides()
    {
        Triangle tri = new Triangle();
        var result = tri.OutputCheck("3.5", "4.25", "5.75");
        Assert.Equal("Разносторонний", result.Item1);
        Assert.Equal(3.5, result.Item2[0].Item1);
        Assert.Equal(100, result.Item2[0].Item2);
    }

    [Fact]
    public void TestValidExcessivelyLargeSides()
    {
        Triangle tri = new Triangle();
        var result = tri.OutputCheck("1000", "2000", "3000");
        Assert.Equal("Разносторонний", result.Item1);
        Assert.Equal(1000, result.Item2[0].Item1);
        Assert.Equal(100, result.Item2[0].Item2);
    }

    [Fact]
    public void TestValidSmallSides()
    {
        Triangle tri = new Triangle();
        var result = tri.OutputCheck("0.01", "0.02", "0.03");
        Assert.Equal("Разносторонний", result.Item1);
        Assert.Equal(0.01, result.Item2[0].Item1);
        Assert.Equal(100, result.Item2[0].Item2);
    }

    [Fact]
    public void TestValidLargeSides()
    {
        Triangle tri = new Triangle();
        var result = tri.OutputCheck("100", "200", "300");
        Assert.Equal("Разносторонний", result.Item1);
        Assert.Equal(100, result.Item2[0].Item1);
        Assert.Equal(100, result.Item2[0].Item2);
    }

    [Fact]
    public void TestValidZeroWidth()
    {
        Triangle tri = new Triangle();
        var result = tri.OutputCheck("0", "5", "5");
        Assert.Equal("Не треугольник", result.Item1);
        Assert.Equal(-1, result.Item2[0].Item1);
        Assert.Equal(-1, result.Item2[0].Item2);
    }

    [Fact]
    public void TestValidZeroHeight()
    {
        Triangle tri = new Triangle();
        var result = tri.OutputCheck("5", "0", "5");
        Assert.Equal("Не треугольник", result.Item1);
        Assert.Equal(-1, result.Item2[0].Item1);
        Assert.Equal(-1, result.Item2[0].Item2);
    }
}

using FluentAssertions;
using WindowLogger.Domain.ValueObjects;

namespace WindowLogger.Domain.Tests.ValueObjects;

public class WindowTitleTests
{
    [Fact]
    public void WindowTitle_正常な値で作成できる()
    {
        // Arrange
        const string VALID_TITLE = "Visual Studio Code";
        
        // Act
        var windowTitle = WindowTitle.Create(VALID_TITLE);
        
        // Assert
        windowTitle.Should().NotBeNull();
        windowTitle.Value.Should().Be(VALID_TITLE);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t")]
    [InlineData("\n")]
    public void WindowTitle_空白文字列では作成できない(string invalidTitle)
    {
        // Act
        var action = () => WindowTitle.Create(invalidTitle);
        
        // Assert
        action.Should().Throw<ArgumentException>()
            .WithMessage("Window title cannot be null or whitespace.");
    }
    
    [Fact]
    public void WindowTitle_null値では作成できない()
    {
        // Act
        var action = () => WindowTitle.Create(null!);
        
        // Assert
        action.Should().Throw<ArgumentException>()
            .WithMessage("Window title cannot be null or whitespace.");
    }
    
    [Fact]
    public void WindowTitle_同じ値のインスタンスは等価である()
    {
        // Arrange
        const string TITLE = "Notepad";
        var windowTitle1 = WindowTitle.Create(TITLE);
        var windowTitle2 = WindowTitle.Create(TITLE);
        
        // Act & Assert
        windowTitle1.Should().Be(windowTitle2);
        windowTitle1.GetHashCode().Should().Be(windowTitle2.GetHashCode());
    }
}
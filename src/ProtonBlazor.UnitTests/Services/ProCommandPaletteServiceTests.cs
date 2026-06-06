// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AwesomeAssertions;
using NUnit.Framework;
using ProtonBlazor;

namespace ProtonBlazor.UnitTests.Services;

[TestFixture]
public class ProCommandPaletteServiceTests
{
    private ProCommandPaletteService _sut = null!;

    [SetUp]
    public void SetUp() => _sut = new ProCommandPaletteService();

    private static ProCommandItem Item(string id, string label, string? description = null, string? group = null) =>
        new() { Id = id, Label = label, Description = description, Group = group, Action = () => Task.CompletedTask };

    // ── Register ─────────────────────────────────────────────────────────────

    [Test]
    public void Register_AddsNewCommand()
    {
        _sut.Register(Item("a", "Alpha"));

        _sut.Search(null).Should().ContainSingle(c => c.Id == "a");
    }

    [Test]
    public void Register_ReplacesExistingById()
    {
        _sut.Register(Item("a", "Alpha"));
        _sut.Register(Item("a", "Alpha Updated"));

        var results = _sut.Search(null);
        results.Should().HaveCount(1);
        results[0].Label.Should().Be("Alpha Updated");
    }

    [Test]
    public void Register_PreservesInsertionOrder()
    {
        _sut.Register(Item("a", "Alpha"));
        _sut.Register(Item("b", "Beta"));
        _sut.Register(Item("c", "Gamma"));

        _sut.Search(null).Select(c => c.Id).Should().Equal("a", "b", "c");
    }

    [Test]
    public void Register_FiresStateChanged()
    {
        var fired = false;
        _sut.StateChanged += () => fired = true;

        _sut.Register(Item("a", "Alpha"));

        fired.Should().BeTrue();
    }

    // ── Unregister ───────────────────────────────────────────────────────────

    [Test]
    public void Unregister_RemovesCommand()
    {
        _sut.Register(Item("a", "Alpha"));
        _sut.Unregister("a");

        _sut.Search(null).Should().BeEmpty();
    }

    [Test]
    public void Unregister_FiresStateChanged()
    {
        _sut.Register(Item("a", "Alpha"));
        var fired = false;
        _sut.StateChanged += () => fired = true;

        _sut.Unregister("a");

        fired.Should().BeTrue();
    }

    [Test]
    public void Unregister_DoesNotFireStateChanged_WhenIdMissing()
    {
        var fired = false;
        _sut.StateChanged += () => fired = true;

        _sut.Unregister("nonexistent");

        fired.Should().BeFalse();
    }

    // ── Search ───────────────────────────────────────────────────────────────

    [TestCase(null)]
    [TestCase("")]
    [TestCase("   ")]
    public void Search_ReturnsAll_WhenQueryBlank(string? query)
    {
        _sut.Register(Item("a", "Alpha"));
        _sut.Register(Item("b", "Beta"));

        _sut.Search(query).Should().HaveCount(2);
    }

    [Test]
    public void Search_FiltersByLabel_CaseInsensitive()
    {
        _sut.Register(Item("a", "Open File"));
        _sut.Register(Item("b", "Close Window"));

        _sut.Search("OPEN").Should().ContainSingle(c => c.Id == "a");
    }

    [Test]
    public void Search_FiltersByDescription_CaseInsensitive()
    {
        _sut.Register(Item("a", "Go", description: "Navigate to a URL"));
        _sut.Register(Item("b", "Stop", description: "Cancel current operation"));

        _sut.Search("navigate").Should().ContainSingle(c => c.Id == "a");
    }

    [Test]
    public void Search_MatchesPartialLabel()
    {
        _sut.Register(Item("a", "Open File Dialog"));

        _sut.Search("file").Should().ContainSingle();
    }

    [Test]
    public void Search_ReturnsEmpty_WhenNoMatch()
    {
        _sut.Register(Item("a", "Alpha"));

        _sut.Search("zzz").Should().BeEmpty();
    }

    // ── Open / Close / Toggle ────────────────────────────────────────────────

    [Test]
    public void IsOpen_DefaultsFalse() => _sut.IsOpen.Should().BeFalse();

    [Test]
    public void Open_SetsIsOpenTrue()
    {
        _sut.Open();
        _sut.IsOpen.Should().BeTrue();
    }

    [Test]
    public void Close_SetsIsOpenFalse()
    {
        _sut.Open();
        _sut.Close();
        _sut.IsOpen.Should().BeFalse();
    }

    [Test]
    public void Toggle_OpensThenCloses()
    {
        _sut.Toggle();
        _sut.IsOpen.Should().BeTrue();

        _sut.Toggle();
        _sut.IsOpen.Should().BeFalse();
    }

    [Test]
    public void Open_FiresStateChanged()
    {
        var fired = false;
        _sut.StateChanged += () => fired = true;
        _sut.Open();
        fired.Should().BeTrue();
    }

    [Test]
    public void Open_DoesNotFireStateChanged_WhenAlreadyOpen()
    {
        _sut.Open();
        var count = 0;
        _sut.StateChanged += () => count++;
        _sut.Open();
        count.Should().Be(0);
    }

    [Test]
    public void Close_DoesNotFireStateChanged_WhenAlreadyClosed()
    {
        var count = 0;
        _sut.StateChanged += () => count++;
        _sut.Close();
        count.Should().Be(0);
    }
}

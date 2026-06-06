// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AwesomeAssertions;
using NUnit.Framework;
using ProtonBlazor;

namespace ProtonBlazor.UnitTests.Services;

[TestFixture]
public class ProAppProgressServiceTests
{
    private ProAppProgressService _sut = null!;

    [SetUp]
    public void SetUp() => _sut = new ProAppProgressService();

    // ── Initial state ────────────────────────────────────────────────────────

    [Test]
    public void IsVisible_DefaultsFalse() => _sut.IsVisible.Should().BeFalse();

    [Test]
    public void IsComplete_DefaultsFalse() => _sut.IsComplete.Should().BeFalse();

    // ── Start ─────────────────────────────────────────────────────────────────

    [Test]
    public void Start_SetsIsVisibleTrue()
    {
        _sut.Start();
        _sut.IsVisible.Should().BeTrue();
    }

    [Test]
    public void Start_SetsIsCompleteFalse()
    {
        _sut.Start();
        _sut.IsComplete.Should().BeFalse();
    }

    [Test]
    public void Start_FiresStateChanged()
    {
        var fired = false;
        _sut.StateChanged += () => fired = true;
        _sut.Start();
        fired.Should().BeTrue();
    }

    [Test]
    public void Start_ResetsIsComplete_WhenCalledWhileComplete()
    {
        _sut.Start();
        _sut.Complete();
        // IsComplete is now true; Start again should reset it.
        _sut.Start();
        _sut.IsComplete.Should().BeFalse();
        _sut.IsVisible.Should().BeTrue();
    }

    // ── Complete ──────────────────────────────────────────────────────────────

    [Test]
    public void Complete_SetsIsCompleteTrue()
    {
        _sut.Start();
        _sut.Complete();
        _sut.IsComplete.Should().BeTrue();
    }

    [Test]
    public void Complete_FiresStateChanged()
    {
        _sut.Start();
        var count = 0;
        _sut.StateChanged += () => count++;
        _sut.Complete();
        count.Should().BeGreaterThan(0);
    }

    [Test]
    public void Complete_IsNoOp_WhenNotVisible()
    {
        var fired = false;
        _sut.StateChanged += () => fired = true;
        _sut.Complete();
        fired.Should().BeFalse();
        _sut.IsComplete.Should().BeFalse();
    }

    [Test]
    public async Task Complete_ResetsStateAfterDelay()
    {
        _sut.Start();
        _sut.Complete();

        // IsVisible still true immediately after Complete
        _sut.IsVisible.Should().BeTrue();

        // After 500 ms the fire-and-forget Task.Delay(450) should have reset state.
        await Task.Delay(600);

        _sut.IsVisible.Should().BeFalse();
        _sut.IsComplete.Should().BeFalse();
    }
}

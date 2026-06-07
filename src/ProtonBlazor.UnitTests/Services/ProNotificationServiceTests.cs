// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AwesomeAssertions;
using NUnit.Framework;
using ProtonBlazor;

namespace ProtonBlazor.UnitTests.Services;

[TestFixture]
public class ProNotificationServiceTests
{
    private ProNotificationService _sut = null!;

    [SetUp]
    public void SetUp() => _sut = new ProNotificationService();

    private static ProNotification Notif(string id, string title = "Title", NotificationSeverity severity = NotificationSeverity.Info) =>
        new() { Id = id, Title = title, Severity = severity };

    // ── Initial state ────────────────────────────────────────────────────────

    [Test]
    public void Notifications_StartsEmpty() => _sut.Notifications.Should().BeEmpty();

    [Test]
    public void UnreadCount_StartsZero() => _sut.UnreadCount.Should().Be(0);

    // ── Add ───────────────────────────────────────────────────────────────────

    [Test]
    public void Add_AppendsToFrontOfList()
    {
        _sut.Add(Notif("a"));
        _sut.Add(Notif("b"));

        _sut.Notifications[0].Id.Should().Be("b");
        _sut.Notifications[1].Id.Should().Be("a");
    }

    [Test]
    public void Add_IncreasesUnreadCount()
    {
        _sut.Add(Notif("a"));
        _sut.Add(Notif("b"));
        _sut.UnreadCount.Should().Be(2);
    }

    [Test]
    public void Add_FiresStateChanged()
    {
        var fired = false;
        _sut.StateChanged += () => fired = true;
        _sut.Add(Notif("a"));
        fired.Should().BeTrue();
    }

    // ── Remove ────────────────────────────────────────────────────────────────

    [Test]
    public void Remove_RemovesById()
    {
        _sut.Add(Notif("a"));
        _sut.Add(Notif("b"));
        _sut.Remove("a");
        _sut.Notifications.Should().ContainSingle(n => n.Id == "b");
    }

    [Test]
    public void Remove_FiresStateChanged()
    {
        _sut.Add(Notif("a"));
        var fired = false;
        _sut.StateChanged += () => fired = true;
        _sut.Remove("a");
        fired.Should().BeTrue();
    }

    [Test]
    public void Remove_DoesNotFire_WhenIdMissing()
    {
        var fired = false;
        _sut.StateChanged += () => fired = true;
        _sut.Remove("nonexistent");
        fired.Should().BeFalse();
    }

    // ── MarkRead ──────────────────────────────────────────────────────────────

    [Test]
    public void MarkRead_MarksNotificationAsRead()
    {
        _sut.Add(Notif("a"));
        _sut.MarkRead("a");
        _sut.Notifications[0].IsRead.Should().BeTrue();
    }

    [Test]
    public void MarkRead_DecreasesUnreadCount()
    {
        _sut.Add(Notif("a"));
        _sut.Add(Notif("b"));
        _sut.MarkRead("a");
        _sut.UnreadCount.Should().Be(1);
    }

    [Test]
    public void MarkRead_FiresStateChanged()
    {
        _sut.Add(Notif("a"));
        var fired = false;
        _sut.StateChanged += () => fired = true;
        _sut.MarkRead("a");
        fired.Should().BeTrue();
    }

    [Test]
    public void MarkRead_DoesNotFire_WhenAlreadyRead()
    {
        _sut.Add(Notif("a"));
        _sut.MarkRead("a");
        var count = 0;
        _sut.StateChanged += () => count++;
        _sut.MarkRead("a");
        count.Should().Be(0);
    }

    // ── MarkAllRead ───────────────────────────────────────────────────────────

    [Test]
    public void MarkAllRead_MarksAllAsRead()
    {
        _sut.Add(Notif("a"));
        _sut.Add(Notif("b"));
        _sut.Add(Notif("c"));
        _sut.MarkAllRead();
        _sut.UnreadCount.Should().Be(0);
        _sut.Notifications.Should().AllSatisfy(n => n.IsRead.Should().BeTrue());
    }

    [Test]
    public void MarkAllRead_FiresStateChanged_WhenAnyUnread()
    {
        _sut.Add(Notif("a"));
        var fired = false;
        _sut.StateChanged += () => fired = true;
        _sut.MarkAllRead();
        fired.Should().BeTrue();
    }

    [Test]
    public void MarkAllRead_DoesNotFire_WhenAllAlreadyRead()
    {
        _sut.Add(Notif("a"));
        _sut.MarkAllRead();
        var count = 0;
        _sut.StateChanged += () => count++;
        _sut.MarkAllRead();
        count.Should().Be(0);
    }

    // ── Clear ─────────────────────────────────────────────────────────────────

    [Test]
    public void Clear_EmptiesList()
    {
        _sut.Add(Notif("a"));
        _sut.Add(Notif("b"));
        _sut.Clear();
        _sut.Notifications.Should().BeEmpty();
    }

    [Test]
    public void Clear_FiresStateChanged()
    {
        _sut.Add(Notif("a"));
        var fired = false;
        _sut.StateChanged += () => fired = true;
        _sut.Clear();
        fired.Should().BeTrue();
    }

    [Test]
    public void Clear_DoesNotFire_WhenAlreadyEmpty()
    {
        var fired = false;
        _sut.StateChanged += () => fired = true;
        _sut.Clear();
        fired.Should().BeFalse();
    }
}

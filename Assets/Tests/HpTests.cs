using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Player
{
    public int Health { get; private set; }

    public Player(int initialHealth)
    {
        Health = initialHealth;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public bool IsDead => Health <= 0;
}
public class HpTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void PlayerTakesDamage()
    {
        // Arrange
        var player = new Player(100);

        // Act
        player.TakeDamage(30);

        // Assert
        Assert.AreEqual(70, player.Health);
    }

    [Test]
    public void PlayerDiesWhenHealthIsZeroOrLess()
    {
        // Arrange
        var player = new Player(50);

        // Act
        player.TakeDamage(50);

        // Assert
        Assert.IsTrue(player.IsDead);
    }
}

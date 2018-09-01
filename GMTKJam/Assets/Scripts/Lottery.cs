using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class Ticket<T>
{
  public T Key { get; private set; }
  public double Weight { get; private set; }
  public Ticket(T key, double weight)
  {
    this.Key = key;
    this.Weight = weight;
  }
}
public class Lottery<T> : IEnumerable<Ticket<T>>
{
  List<Ticket<T>> tickets = new List<Ticket<T>>();
  static Random rand = new Random();
  public void Add(T key, double weight)
  {
    tickets.Add(new Ticket<T>(key, weight));
  }
  public Ticket<T> Draw(bool removeWinner)
  {
    double r = rand.NextDouble() * tickets.Sum(a => a.Weight);
    double min = 0;
    double max = 0;
    Ticket<T> winner = null;
    foreach (var ticket in tickets)
    {
      max += ticket.Weight;
      //-----------
      if (min <= r && r < max)
      {
        winner = ticket;
        break;
      }
      //-----------
      min = max;
    }
    if (winner == null) throw new Exception();
    if (removeWinner) tickets.Remove(winner);
    return winner;
  }

  public IEnumerator<Ticket<T>> GetEnumerator()
  {
    return tickets.GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }
}
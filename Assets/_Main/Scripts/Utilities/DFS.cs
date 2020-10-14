﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFS<T>
{
   public delegate bool Satisfies(T curr);
   public delegate List<T> GetNeighbours(T curr);
   
   public List<T> Run(T start, Satisfies satisfies, GetNeighbours getNeighbours, int watchdog = 50)
   {
      Dictionary<T, T> parents = new Dictionary<T, T>();
      Stack<T> pending = new Stack<T>();
      HashSet<T> visited = new HashSet<T>();
      pending.Push(start);
      while (pending.Count != 0)
      {
         watchdog--;
         if (watchdog <= 0)
         {
            return new List<T>();
         }
         T current = pending.Pop();
         if (satisfies(current))
         {
            List<T> path = new List<T>();
            path.Add(current);
            while (parents.ContainsKey(path[path.Count - 1]))
            {
               var lastNode = path[path.Count - 1];
               path.Add(parents[lastNode]);
               path.Reverse();
               return path;
            }
         }
         visited.Add(current);
         List<T> neighbours = getNeighbours(current);
         for (int i = 0; i < neighbours.Count; i++)
         {
            var item = neighbours[i];
            if (visited.Contains(item) || pending.Contains(item))
            {
               continue;               
            }
            pending.Push(item);
            parents[item] = current;
         }
      }
      return new List<T>();
   }  
}

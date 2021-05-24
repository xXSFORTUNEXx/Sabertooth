//X:
for (int i = 0; i < 10; i++)
{
	if (s_Map[mapNum].m_MapNpc[i].IsSpawned)
	{
		if ((X - 1) == s_Map[mapNum].m_MapNpc[i].X && Y == s_Map[mapNum].m_MapNpc[i].Y)
		{
			Direction = (int)Directions.Left;
			DidMove = true;
			return;
		}
	}
}
for (int i = 0; i < 20; i++)
{
	if (s_Map[mapNum].r_MapNpc[i].IsSpawned)
	{
		if ((X - 1) == s_Map[mapNum].r_MapNpc[i].X && Y == s_Map[mapNum].r_MapNpc[i].Y)
		{
			Direction = (int)Directions.Left;
			DidMove = true;
			return;
		}
	}
}
for (int p = 0; p < 5; p++)
{
	if (s_Player[p].Connection != null)
	{
		if ((X - 1) == (s_Player[p].X + 12) && Y == (s_Player[p].Y + 9))
		{
			Direction = (int)Directions.Left;
			DidMove = true;
			if (p == Target) { AttackPlayer(s_Server, s_Player, p); }                             
			return;
		}
	}
}
//
for (int i = 0; i < 10; i++)
{
	if (s_Map[mapNum].m_MapNpc[i].IsSpawned)
	{
		if ((X + 1) == s_Map[mapNum].m_MapNpc[i].X && Y == s_Map[mapNum].m_MapNpc[i].Y)
		{
			Direction = (int)Directions.Right;
			DidMove = true;
			return;
		}
	}
}
for (int i = 0; i < 20; i++)
{
	if (s_Map[mapNum].r_MapNpc[i].IsSpawned)
	{
		if ((X + 1) == s_Map[mapNum].r_MapNpc[i].X && Y == s_Map[mapNum].r_MapNpc[i].Y)
		{
			Direction = (int)Directions.Right;
			DidMove = true;
			return;
		}
	}
}
for (int p = 0; p < 5; p++)
{
	if (s_Player[p].Connection != null)
	{
		if ((X + 1) == (s_Player[p].X + 12) && Y == (s_Player[p].Y + 9))
		{
			Direction = (int)Directions.Right;
			DidMove = true;
			if (p == Target) { AttackPlayer(s_Server, s_Player, p); }
			return;
		}
	}
}
//Y:
for (int i = 0; i < 10; i++)
{
	if (s_Map[mapNum].m_MapNpc[i].IsSpawned)
	{
		if ((Y - 1) == s_Map[mapNum].m_MapNpc[i].Y && X == s_Map[mapNum].m_MapNpc[i].X)
		{
			Direction = (int)Directions.Up;
			DidMove = true;
			return;
		}
	}
}
for (int i = 0; i < 20; i++)
{
	if (s_Map[mapNum].r_MapNpc[i].IsSpawned)
	{
		if ((Y - 1) == s_Map[mapNum].r_MapNpc[i].Y && X == s_Map[mapNum].r_MapNpc[i].X)
		{
			Direction = (int)Directions.Up;
			DidMove = true;
			return;
		}
	}
}
for (int p = 0; p < 5; p++)
{
	if (s_Player[p].Connection != null)
	{
		if ((Y - 1) == (s_Player[p].Y + 9) && X == (s_Player[p].X + 12))
		{
			Direction = (int)Directions.Up;
			DidMove = true;
			if (p == Target) { AttackPlayer(s_Server, s_Player, p); }
			return;
		}
	}
}
//
for (int i = 0; i < 10; i++)
{
	if (s_Map[mapNum].m_MapNpc[i].IsSpawned)
	{
		if ((Y + 1) == s_Map[mapNum].m_MapNpc[i].Y && X == s_Map[mapNum].m_MapNpc[i].X)
		{
			Direction = (int)Directions.Down;
			DidMove = true;
			return;
		}
	}
}
for (int i = 0; i < 20; i++)
{
	if (s_Map[mapNum].r_MapNpc[i].IsSpawned)
	{
		if ((Y + 1) == s_Map[mapNum].r_MapNpc[i].Y && X == s_Map[mapNum].r_MapNpc[i].X)
		{
			Direction = (int)Directions.Down;
			DidMove = true;
			return;
		}
	}
}
for (int p = 0; p < 5; p++)
{
	if (s_Player[p].Connection != null)
	{
		if ((Y + 1) == (s_Player[p].Y + 9) && X == (s_Player[p].X + 12))
		{
			Direction = (int)Directions.Down;
			DidMove = true;
			if (p == Target) { AttackPlayer(s_Server, s_Player, p); }
			return;
		}
	}
}
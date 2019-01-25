                #region Dynamic Commands
                //Add a latency to all clients for testing
                if (input.Length >= 11 && input.Substring(0, 10) == "minlatency")
                {
                    string floatseconds = input.Substring(11);
                    int intseconds = ToInt32(floatseconds);
                    string mseconds = floatseconds.Insert(0, "0.0");
                    float delay = ToSingle(mseconds);

                    if (intseconds >= 15 || intseconds == 0)
                    {
                        SabertoothServer.netServer.Configuration.SimulatedMinimumLatency = delay;
                        Logging.WriteMessageLog("Minimum latency is now " + floatseconds + "ms");
                    }
                    else
                    {
                        Logging.WriteMessageLog("Value must be greater or equal to 14, value can be 0 to remove latency");
                    }
                    isDynamic = true;
                }

                //Checks for a 4 octect ip address (wrote this cause I wasnt paying attention to how the server pulls its ip from the host)
                if (input.Length >= 13 && input.Substring(0, 12) == "newnetserver")
                {
                    string ipaddress = input.Substring(13); //Create substring of the IP address
                    string[] octect = ipaddress.Split('.'); //Make sure we have 4 octets by splitting the ip address into seperate strings for each octet
                    bool[] failed = { false, false };
                    int check = 0;

                    if (octect.Length == 4) { failed[0] = true; }
                    if (failed[0] != false)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (octect[i] != null && octect[i] != "")
                            {
                                if (ToInt32(octect[i]) >= 0 && ToInt32(octect[i]) <= 255)
                                {
                                    check += 1;
                                }
                            }
                        }
                    }

                    if (check == 4) { failed[1] = true; }

                    if (failed[0] == true && failed[1] == true)
                    {
                        Console.WriteLine("Valid IP: " + ipaddress);
                        
                    }
                    else
                    {
                        Console.WriteLine("Invalid IP: " + ipaddress);
                    }
                    isDynamic = true;
                }

                if (input.Length >= 11 && input.Substring(0, 10) == "sqlcommand")
                {
                    string command = input.Substring(11);
                    try
                    {
                        string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
                        using (var sql = new SqlConnection(connection))
                        {
                            sql.Open();
                            using (var cmd = new SqlCommand(command, sql))
                            {
                                cmd.ExecuteNonQuery();
                                Logging.WriteMessageLog("Command: " + cmd.CommandText);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Logging.WriteMessageLog(e.Message, "SQL");
                    }
                    isDynamic = true;
                }

                if (input.Length >= 7 && input.Substring(0, 7) == "account")    //Check for account command
                {
                    if (input.Substring(8, 6) == "create")    //Create
                    {
                        if (input.Length >= 14)
                        {
                            string restofInfo = input.Substring(14);  //Get whats left of the string after account create (username and pass)  
                            string[] finalInfo = restofInfo.Split(' '); //Split the username and password into their own strings
                            if (finalInfo[1].Length >= 3 && finalInfo[2].Length >= 3 && finalInfo[3].Length >= 3)   //Make sure they are both at least three characters long
                            {
                                Player ac_Player = new Player(finalInfo[1], finalInfo[2], finalInfo[3], 0, 0, 0, 0, 0, 1, 100, 100, 100, 0,
                                                                100, 10, 100, 100, 5, 5, 5, 5, 1000);   //Create the player in an array so we can save it
                                ac_Player.CreatePlayerInDatabase();
                                Logging.WriteMessageLog("Account create! Username: " + finalInfo[1] + ", Password: " + finalInfo[2], "Commands"); //Let the operator know
                            }
                            else { Logging.WriteMessageLog("USERNAME and PASSWORD must be 3 characters each!", "Commands"); } //Dont fuck it up by making basic shit

                        }
                    }
                    else if (input.Substring(8, 6) == "delete")
                    {
                        if (input.Length >= 14)
                        {
                            string restofInfo = input.Substring(14);
                            if (AccountExist(restofInfo))
                            {
                                Console.Write("Are you sure? (y/n)");
                                string answer = Console.ReadLine();
                                if (answer == "y") { Delete("Players / " + restofInfo + ".xml"); return; }
                            }
                            else { Logging.WriteMessageLog("Account doesnt exist!", "Commands"); return; }
                        }
                    }
                    else { Logging.WriteMessageLog("Please enter a valid command!", "Commands"); return; }  //Did you provide a modifier?
                    isDynamic = true;
                }
                #endregion
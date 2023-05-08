using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RhythmsGonnaGetYou
{
    class Program
    {
        // User string input
        static string PromptForString(string prompt)
        {
            Console.Write(prompt);
            var userInput = Console.ReadLine();

            return userInput;
        }

        //user int input
        static int PromptForInteger(string prompt)
        {
            var isThisGoodInput = false;
            do
            {
                var stringInput = PromptForString(prompt);

                int numberInput;
                isThisGoodInput = Int32.TryParse(stringInput, out numberInput);

                if (isThisGoodInput)
                {   //So far so good, store the number in numberInput for use.
                    return numberInput;
                }
                else
                {   //Ooh ooh not a valid input from the user.
                    Console.WriteLine("Sorry, but that is not a valid input - try again.");
                }
            } while (!isThisGoodInput);
            //C# demands we have a return present.
            return 0;
        }

        //user Bool Input 
        public static bool getBoolInputValue(string IsSigned)
        {
            var IsSignedToUpper = IsSigned.ToUpper();

            if (IsSigned.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else if (IsSigned.Equals("N", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            else
            {
                return false;
            }
        }



        static void Main(string[] args)
        {

            var context = new AlbumContext();

            var keepGoing = true;

            while (keepGoing)
            {
                Greeting();
                var userInputMenu = Convert.ToDouble(Console.ReadLine());


                if (userInputMenu == 0)
                {
                    keepGoing = false;
                }
                else if (userInputMenu == 1)
                {
                    var nameBandToSearch = PromptForString("What is the Bands name?: ");

                    var checkIfExistingBand = context.Band.FirstOrDefault(band => band.Name == nameBandToSearch);
                    if (checkIfExistingBand != null)
                    {
                        Console.WriteLine($"Band name {nameBandToSearch} is already in Database");
                    }
                    else
                    {
                        Console.WriteLine($"New Band in database - {nameBandToSearch}");

                        var newbandName = nameBandToSearch;

                        var countryOfOrigin = PromptForString($"What is {nameBandToSearch} country of Origin?: ");

                        var amountOfMembers = PromptForInteger("How many Band Members are there?: ");

                        var websiteOfBand = PromptForString("What is the website of the band?: ");

                        var style = PromptForString($"What is the Style of music that {nameBandToSearch} Plays?: ");

                        var signed = getBoolInputValue($"Is {nameBandToSearch} Signed to a Record Label: (Y)Yes or (N)No");

                        var contactName = PromptForString("What is the Contact name for the Band?: ");


                        var newBand = new Bands
                        {
                            Name = newbandName,
                            CountryOfOrigin = countryOfOrigin,
                            NumberOfMembers = amountOfMembers,
                            Website = websiteOfBand,
                            Style = style,
                            IsSigned = signed,
                            contactname = contactName

                        };

                        context.Band.Add(newBand);
                        context.SaveChanges();

                        Console.WriteLine($"Saved {nameBandToSearch} to database discography.\n Going back to Main Menu");
                    }

                }
                else if (userInputMenu == 2)
                {

                    Console.WriteLine("What is the name of the album you want to add? ");
                    var searchAlbums = PromptForString("> : ");

                    var existingAlbum = context.Album.FirstOrDefault(Albums => Albums.Title == searchAlbums);

                    if (existingAlbum != null)
                    {
                        Console.WriteLine($"{searchAlbums} is already in existence. Please try again. ");
                    }
                    else
                    {
                        var searchBands = PromptForString("What Band made this ALbum?:  ");

                        var doesBandExist = context.Band.FirstOrDefault(Bands => Bands.Name == searchBands);

                        if (doesBandExist == null)
                        {
                            Console.WriteLine($"{searchBands} does not exist. Please add the band first to Continue. ");
                        }

                        else
                        {

                            Console.WriteLine($"Band name: {searchBands}");

                            Console.WriteLine($"Album name: {searchAlbums}");
                            var albumTitle = searchAlbums;

                            var isExplicit = getBoolInputValue("Is the Album Explicit: (Y)Yes or (N)No? ");

                            var yearOfRelease = PromptForInteger("What year was it Released?: ");


                            var newAlbum = new Albums
                            {
                                Title = albumTitle,
                                IsExplicit = isExplicit,
                                YearReleased = yearOfRelease,
                                BandId = doesBandExist.Id
                            };

                            //You need to save this to the Beekeeper Database
                            //Save the new album to the database
                            context.Album.Add(newAlbum);
                            context.SaveChanges();
                            Console.WriteLine($"\nYour new album titled {albumTitle} has been successfully added. ");
                        }
                    }
                }
                else if (userInputMenu == 3)
                {
                    var searchSong = PromptForString("What is the name of the Song you want to Add?: ");

                    var doesSongExist = context.Song.FirstOrDefault(song => song.Title == searchSong);


                    if (doesSongExist != null)
                    {
                        Console.WriteLine($"{searchSong} already Exist in the Database.");
                    }
                    else
                    {
                        var searchAlbums = PromptForString("What is the name of the Album the Song will go in?: ");

                        var doesAlbumExist = context.Album.FirstOrDefault(album => album.Title == searchAlbums);

                        if (doesAlbumExist == null)
                        {
                            Console.WriteLine($"{searchAlbums} this Album does not Exist in the Database. Please Add it first!s");
                        }
                        else
                        {
                            Console.WriteLine($"Album Title: {searchAlbums} ");
                            Console.WriteLine($"Song Title: {searchSong} ");

                            var trackNumber = PromptForInteger("What track number is the Song?: ");
                            var secondsOfTheSong = PromptForInteger("How long is the Song in Seconds?: ");

                            var newSong = new Songs
                            {
                                TrackNumber = trackNumber,
                                Duration = secondsOfTheSong,
                                Title = searchSong,
                                AlbumId = doesAlbumExist.Id

                            };
                            //Save the new song to the database.
                            context.Song.Add(newSong);
                            context.SaveChanges();

                            Console.WriteLine($"You have successfully added {searchSong} into {searchAlbums}");

                        }
                    }

                }
                else if (userInputMenu == 4)
                {
                    var searchBand = PromptForString("What is the Bands Name that Will be UN- Signed?: ");

                    var doesBandExist = context.Band.FirstOrDefault(band => band.Name == searchBand);

                    if (doesBandExist == null)
                    {
                        Console.WriteLine($"Band {searchBand} does not Exist. Please add the Band.");
                    }
                    else
                    {
                        doesBandExist.IsSigned = false;

                        context.SaveChanges();

                        Console.WriteLine($"{searchBand} has been UN-Signed.");
                    }
                }
                else if (userInputMenu == 5)
                {
                    var searchBand = PromptForString("What Band do you want to SIGN?: ");
                    var doesBandExist = context.Band.FirstOrDefault(band => band.Name == searchBand);
                    if (doesBandExist == null)
                    {
                        Console.WriteLine($"{searchBand} needs to be Added first!");
                    }
                    else
                    {
                        doesBandExist.IsSigned = true;
                        context.SaveChanges();

                        Console.WriteLine($"{searchBand} is now SIGNED successfully!");
                    }
                }
                else if (userInputMenu == 6)
                {
                    foreach (var band in context.Band)
                    {
                        Console.WriteLine($"Band/Artist {band.Name} is in the Database");
                    }
                }
                else if (userInputMenu == 8)
                {

                    List<Albums> albumsList = context.Album.OrderBy(album => album.YearReleased).ToList();

                    foreach (Albums album in albumsList)
                    {
                        Console.WriteLine($"{album.Title} was released on {album.YearReleased}");
                    }


                }





























                //var AlbumCount = context.Album.Count();

                //Console.WriteLine($"There are {AlbumCount} in album database");


                //var bandList = context.Band.Include(band => band.Album).ThenInclude(album => album.Song).ToList();

                //foreach (Bands b in bandList)
                // {
                //  Console.WriteLine($"Band name is {b.Name}");

                //foreach (Albums a in b.Album)
                //{
                //  Console.WriteLine($"The album {a.Title} is by the band {b.Name}");
                //foreach (Songs s in a.Song)
                //{
                //  Console.WriteLine($"The song {s.Title} is on the album {a.Title}");
                // }


                // }

                // }


            }

            //Greetings 
            static void Greeting()
            {
                Console.WriteLine("--------------------------------------------|");
                Console.WriteLine($"          Rhythms Gonna Get You              )");
                Console.WriteLine($"        *** Music Discography ***            )");
                Console.WriteLine($"                                             )");
                Console.WriteLine($"____________________________________________|");
                Console.WriteLine($"");
                Console.WriteLine($"--------------------------------------------|");
                Console.WriteLine($"             Album/Band/Song Menu                                  ");
                Console.WriteLine($"");
                Console.WriteLine($"(1) Add Band \n(2) Add Album \n(3) Add Song \n(4) Re-Sign a Band \n(5) Un-Sign a Band \n(0) Quit");
                Console.WriteLine($"");
                Console.WriteLine($"____________________________________________|");
                Console.WriteLine($"");
                Console.WriteLine($"--------------------------------------------|");
                Console.WriteLine($"             Music In The Database Menu      ");
                Console.WriteLine($"                                              ");
                Console.WriteLine($"(6) View All Bands \n(7) View All Albums By Their Band \n(8) View All Bands By Release Year \n(9) View All Signed Bands \n(10) View All Un-Signed Bands \n(0) Quit");
                Console.WriteLine($"");
                Console.WriteLine($"_____________________________________________|");
                Console.WriteLine($"");




            }


        }
    }
}
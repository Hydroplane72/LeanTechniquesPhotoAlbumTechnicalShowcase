using LeanTechniquesPhotoAlbumTechnicalShowcase.Models;
using System;
using System.Collections.Generic;

namespace LeanTechniquesPhotoAlbumTechnicalShowcase
{
    internal class Program
    {
        static void Main(string[] args)
        {


            /*TODO: Thoughts
             * Need to ask if they already know the album they would like
             * 1) Look up by name of photo?
             * 2) Look up by id
             * 3) Show options
             * 4) Show all albums and photos
             * 
             * GetAllPhotosFromURL
             * Order Photos into albums
             *  Output Albums (in order)
             *      Output Photos (in order) by album
             *      
             *      
             *Classes and packages probably needed
             *  Classes and services:
             *      Program - Service
             *          Starting thread.
             *          Controls general flow of program
             *              Ask user first question
             *              Show to user results
             *              Wait for user responsed before close
             *      CommonHelper - Service
             *          Cleanup
             *          Logging
             *      Photo - Class
             *          albumid
             *          id
             *          title
             *          url
             *          thumbnail
             *      PhotosAPIClient - Service
             *          Contains the calls that I will need to get the photos from the API and return them in a list
             *              Get by ID
             *              Get by name?
             *              Filter by ID and Name?
             *              Get all
             *              Returns list or dictionary<string, dictionary<string, photo>>? (dictionary<albumId, dictionary<photoID, photo>>)
             *      PhotoAlbumService - Service
             *          Main worker
             *          Deals with PhotosAPIClient and getting data from them
             *          Deals with organizing data returned to us by api.
             *          Returns to Program the Dictionary Collection returned.
             *          IF at any point in time the user asks for a full download of data. The data will be stored in this class
             *          
             *              
             *      PhotoAlbumServiceTester - Unit testing
             *          Test PhotoAlbumServiceFunctions
             *
             * Packages:
             * RestSharp
             * Newtonsoft
             *
             *      
             *      
             */

            string userInput = "";

            while (true)
            {
                using (Services.AlbumService albumService = new Services.AlbumService())
                {
                    userInput = AskUserForDirections();

                    switch (userInput)
                    {
                        case "1":
                            //No need to worry about parsing int here. Already checked that previously
                            List<Models.Album> albumList = albumService.GetAllAlbums();

                            if (albumService.Status.IsSuccessfull == true)
                            {
                                Console.Clear();
                                Console.WriteLine("Below is a full list of all albums available");
                                foreach (Album album in albumList)
                                {
                                    Console.WriteLine("[" + album.Id + "] " + album.Title);
                                }

                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Error hit:");
                                Console.WriteLine(albumService.Status.GetErrors());  
                            }

                            Console.WriteLine();
                            Console.WriteLine("Press any key to return to home screen.");
                            Console.ReadKey();

                            break;

                        case "2":
                            userInput = AskForAlbumID();

                            if (userInput == "-1")
                            {
                                //Get out and return to main menu
                                break;
                            }
                            //No need to worry about parsing int here. Already checked that previously
                            List<Models.Photo> photoList = albumService.GetPhotosForAlbum(int.Parse(userInput));

                            if (albumService.Status.IsSuccessfull == true)
                            {
                                Console.Clear();
                                OutputPhotoList( photoList);

                                Console.WriteLine();
                                Console.WriteLine("End of list. Press any key to continue.");

                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("Error hit:");
                                Console.WriteLine(albumService.Status.GetErrors());
                                Console.WriteLine("Press any key to return to home screen.");
                                Console.ReadKey();
                            }
                            break;

                        case "3":
                            //No need to worry about parsing int here. Already checked that previously
                            Dictionary<int, Dictionary<int, Photo>> albumPhotoList = albumService.GetAllAlbumsAndPhotos();

                            if (albumService.Status.IsSuccessfull == true)
                            {
                                Console.Clear();
                                foreach (Dictionary<int, Photo> photos in albumPhotoList.Values)
                                {
                                    OutputPhotoList(new List<Photo>(photos.Values));
                                }

                                Console.WriteLine();
                                Console.WriteLine("End of list. Press any key to continue.");

                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("Error hit:");
                                Console.WriteLine(albumService.Status.GetErrors());
                                Console.WriteLine("Press any key to return to home screen.");
                                Console.ReadKey();
                            }
                            break;

                        case "4":
                            //User asked to exit
                            return;
                        default:
                            break;
                    }
                }
            }
        }

        private static void OutputPhotoList( List<Photo> pPhotoList)
        {
            try
            {
                if (pPhotoList.Count > 0)
                {
                    //Write out the photo album id
                    Console.WriteLine("photo-album " + pPhotoList[0].AlbumId);

                    //Write out the photos for the album
                    foreach (Photo photo in pPhotoList)
                    {
                        Console.WriteLine("[" + photo.Id + "] " + photo.Title);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception hit: " + ex.Message);
                Console.WriteLine("Please press any key to retry");
                Console.ReadKey();
            }
        }

        private static string AskForAlbumID()
        {
            string userInput = "";
            int userInputValidated = 0;

            //Ask for an album ID
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("To go back enter -1.");
                    Console.WriteLine("Please enter an album ID: ");
                    userInput = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(userInput) == true)
                    {
                        //Have them retry
                        continue;
                    }
                    else if (userInput == "-1")
                    {
                        break;
                    }
                    else if (int.TryParse(userInput, out userInputValidated) == true)
                    {
                        //Is valid input return
                        return userInput;
                    }
                    else
                    {
                        //Is not a valid input ask user again
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception hit: " + ex.Message);
                    Console.WriteLine("Please press any key to retry");
                    Console.ReadKey();
                }
            }
            return userInput;
        }

        private static string AskUserForDirections()
        {
            string userInput = "";

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Options:");
                Console.WriteLine("1) Provide all album options.");
                Console.WriteLine("2) Enter an AlbumID.");
                Console.WriteLine("3) Show all Albums and their photos.");
                Console.WriteLine("4) Exit");

                try
                {



                    userInput = Console.ReadLine();

                    //Make sure is a valid string
                    if (string.IsNullOrWhiteSpace(userInput) == true)
                    {
                        continue;
                    }
                    else if (userInput.Equals("1") == true || userInput.Equals("2") == true || userInput.Equals("3") == true || userInput.Equals("4") == true)
                    {
                        return userInput;
                    }
                    else
                    {
                        //Is not a valid option return them to the options
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception hit: " + ex.Message);
                    Console.WriteLine("Please press any key to retry");
                    Console.ReadKey();
                }
            }



        }
    }
}

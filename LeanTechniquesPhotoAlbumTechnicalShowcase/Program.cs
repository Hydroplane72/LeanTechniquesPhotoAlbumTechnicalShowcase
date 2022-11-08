using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}

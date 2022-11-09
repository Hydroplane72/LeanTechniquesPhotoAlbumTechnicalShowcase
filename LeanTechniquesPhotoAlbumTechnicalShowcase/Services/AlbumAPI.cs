using LeanTechniquesPhotoAlbumTechnicalShowcase.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace LeanTechniquesPhotoAlbumTechnicalShowcase.Services
{
    /// <summary>
    /// Connects the the API and gets info for Albums and photos
    /// </summary>
    public class AlbumAPI : IDisposable
    {

        #region "IDisposable"

        private bool _disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                
                if (mRestSharp != null)
                {
                    mRestSharp.Dispose();
                }
                
            }


            _disposed = true;
        }
        #endregion

        #region "Variables"


        private readonly RestClient mRestSharp;


        private const string mSource = "LeanTechniquesPhotoAlbumTechnicalShowcase.Services.AlbumAPI.";

        #endregion

        #region "Properties"
        private readonly Status mStatus = new Status(true);

        public Status Status
        {
            get { return mStatus; }
        }

        #endregion

        #region "Constructors"


        public AlbumAPI()
        {
            const string source = mSource + "AlbumAPI()";

            try
            {
                const string mBaseURL = "https://jsonplaceholder.typicode.com/";

                mRestSharp = new RestClient(mBaseURL);
            }
            catch (Exception ex)
            {
                mStatus.IsSuccessfull = false;
                mStatus.AddNewErrorMessage(source, ex.Message);
            }
        }
        #endregion

        #region "Methods"


        /// <summary>
        /// Gets a list of Photos limited by the AlbumID
        /// </summary>
        /// <param name="pAlbumID"></param>
        /// <returns>List of Photos for that Album</returns>
        public List<Models.Photo> GetPhotosByAlbumID(int pAlbumID)
        {
            const string source = mSource + "GetPhotosByAlbumID(string pAlbumID)";

            List<Models.Photo> photos = new List<Models.Photo>();

            try
            {
                //Set the request
                RestRequest restRequest = new RestRequest("photos", Method.Get);

                //Add parameter passed in
                restRequest.AddQueryParameter("albumId", pAlbumID);

                //Get the data
                RestResponse response = mRestSharp.ExecuteGet(restRequest);

                //check response
                if (response.IsSuccessful == true)
                {
                    photos = JsonConvert.DeserializeObject<List<Models.Photo>>(response.Content);
                }
                else
                {
                    mStatus.IsSuccessfull = false;
                    mStatus.AddNewErrorMessage(source, "Error trying to get photos. " + response.Content);
                }
            }
            catch (Exception ex)
            {
                mStatus.IsSuccessfull = false;
                mStatus.AddNewErrorMessage(source, ex.Message);
            }

            return photos;
        }

        /// <summary>
        /// Gets all photos no matter the AlbumID
        /// </summary>
        /// <returns>The entire list of photos available by the endpoint</returns>
        public List<Photo> GetAllPhotos()
        {

            const string source = mSource + "GetAllPhotos()";

            List<Models.Photo> photos = new List<Models.Photo>();

            try
            {
                //Set the request
                RestRequest restRequest = new RestRequest("photos", Method.Get);


                //Get the data
                RestResponse response = mRestSharp.ExecuteGet(restRequest);

                //check response
                if (response.IsSuccessful == true)
                {
                    photos = JsonConvert.DeserializeObject<List<Models.Photo>>(response.Content);
                }
                else
                {
                    mStatus.IsSuccessfull = false;
                    mStatus.AddNewErrorMessage(source, "Error trying to get photos. " + response.Content);
                }
            }
            catch (Exception ex)
            {
                mStatus.IsSuccessfull = false;
                mStatus.AddNewErrorMessage(source, ex.Message);
            }

            return photos;
        }


        public List<Album> GetAllAlbums()
        {

            const string source = mSource + "GetAllAlbums()";

            List<Models.Album> albums = new List<Models.Album>();

            try
            {
                //Set the request
                RestRequest restRequest = new RestRequest("albums", Method.Get);


                //Get the data
                RestResponse response = mRestSharp.ExecuteGet(restRequest);

                //check response
                if (response.IsSuccessful == true)
                {
                    albums = JsonConvert.DeserializeObject<List<Models.Album>>(response.Content);
                }
                else
                {
                    mStatus.IsSuccessfull = false;
                    mStatus.AddNewErrorMessage(source, "Error trying to get Albums. " + response.Content);
                }
            }
            catch (Exception ex)
            {
                mStatus.IsSuccessfull = false;
                mStatus.AddNewErrorMessage(source, ex.Message);
            }

            return albums;
        }
        #endregion
    }
}

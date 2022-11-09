using LeanTechniquesPhotoAlbumTechnicalShowcase.Models;
using System;
using System.Collections.Generic;

namespace LeanTechniquesPhotoAlbumTechnicalShowcase.Services
{
    /// <summary>
    /// <para> Uses the <see cref="AlbumAPI"/> service to get the data from the API.
    /// It then saves that data in memory so the next time we need it we will have it.
    /// During Testing I noticed that the data from the API does not change overtime.
    /// I chose to keep the previous calls in memory to save time from having to hit the API for data again when not needed.
    /// Since the data doesn't change I could have saved the response to a file on PC so I would be able to get again later but thought that was going too far.
    /// I could have also just loaded the entire collection at the beginning but think that is unnessary if the user already knows the info they want to look for.</para>
    /// 
    /// <para>
    /// Remember to check <see cref="Status"/> after each all in this object to make sure no errors were hit.
    /// </para>
    /// </summary>
    internal class AlbumService : IDisposable
    {

        #region "IDisposable"

        private bool _disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                if(mAlbumAPI != null)
                {
                    mAlbumAPI.Dispose();
                }

                if(PhotoAlbumHistory != null)
                {
                    PhotoAlbumHistory.Clear();
                }
            }

            _disposed = true;
        }
        #endregion

        #region "Variables"

        private const string mSource = "LeanTechniquesPhotoAlbumTechnicalShowcase.Services.AlbumService.";

        private readonly AlbumAPI mAlbumAPI = new AlbumAPI();


        

        #endregion

        #region "Properties"
        private  Status mStatus = new Status(true);

        internal Status Status
        {
            get { return mStatus; }
        }

        /// <summary>
        /// key = AlbumID <br />
        /// Value = List of photos for Album <br />
        /// <para>
        /// Since there is no need to search the list of photos at this time I have the photos as a list. 
        /// If I need to ever search the photos by ID then I will want to make a dictionary as well.</para>
        /// </summary>
        internal Dictionary<int, Dictionary<int, Photo>> PhotoAlbumHistory = new Dictionary<int, Dictionary<int, Photo>>();
        #endregion

        #region "Constructors"


        internal AlbumService()
        {

        }

        
        #endregion

        #region "Methods"
        /// <summary>
        /// Gets the photos for an album
        /// </summary>
        /// <param name="pAlbumID"></param>
        /// <returns></returns>
        internal List<Photo> PhotosForAlbum(int pAlbumID)
        {
            List<Photo> photosForAlbum = new List<Photo>();

            const string source = mSource + "PhotosForAlbum(string pAlbumID)";

            try
            {
                //First make sure we have not recieved previously
                if (PhotoAlbumHistory.ContainsKey(pAlbumID) == true && PhotoAlbumHistory[pAlbumID].Values.Count != 0)
                {
                    //Data Exists in memory use it
                    photosForAlbum.AddRange(PhotoAlbumHistory[pAlbumID].Values);

                }
                else
                {
                    //Data does not exist in memory. Get it from api.

                    //We have not retrieved in past. Go and get now
                    photosForAlbum = mAlbumAPI.GetPhotosByAlbumID(pAlbumID);

                    //Make sure we were successful
                    if (mAlbumAPI.Status.IsSuccessfull == false)
                    {
                        mStatus = mAlbumAPI.Status;

                    }

                    if (mStatus.IsSuccessfull == true)
                    {
                        //Add to collection for next time
                        AddPhotoListToAlbumHistory(ref photosForAlbum);
                    }
                }
            }
            catch (Exception ex)
            {
                mStatus.IsSuccessfull = false;
                mStatus.AddNewErrorMessage(source, ex.Message);
            }

            return photosForAlbum;
        }

        /// <summary>
        /// During testing I did not see any duplicate photo IDs for an album but this is just to make sure.
        /// </summary>
        /// <param name="photosForAlbum">List of photos to add to dictionary. Any dups will be logged in <see cref="Status"/> but not added.</param>
        private void AddPhotoListToAlbumHistory(ref List<Photo> photosForAlbum)
        {
            const string source = mSource + "AddPhotoListToAlbumHistory";

            try
            {
                foreach (Photo photo in photosForAlbum)
                {
                    //If album exist but not photo then add photo
                    if (PhotoAlbumHistory.ContainsKey(photo.AlbumId)== true && PhotoAlbumHistory[photo.AlbumId].ContainsKey(photo.Id) == false)
                    {
                        PhotoAlbumHistory[photo.AlbumId].Add(photo.Id, photo);
                    }
                    else if (PhotoAlbumHistory.ContainsKey(photo.AlbumId) == false)
                        //Does not contain album yet
                    {
                        PhotoAlbumHistory.Add(photo.AlbumId, new Dictionary<int, Photo>());
                        PhotoAlbumHistory[photo.AlbumId].Add(photo.Id, photo);
                    }
                    else
                    {
                        mStatus.AddNewErrorMessage(source, "Duplicate PhotoID found returned for album. AlbumID: " + photo.AlbumId + " PhotoID: " + photo.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                mStatus.IsSuccessfull = false;
                mStatus.AddNewErrorMessage(source, ex.Message);
            }
        }

        /// <summary>
        /// Returns all Photos and Albums
        /// </summary>
        /// <returns>Fills this object out then returns. <seealso cref="PhotoAlbumHistory"/></returns>
        internal Dictionary<int, Dictionary<int,Photo>> GetAllAlbumsAndPhotos()
        {
            const string source = mSource + "GetAllAlbumsAndPhotos";

            List<Photo> photos;

            try
            {
                photos = mAlbumAPI.GetAllPhotos();

                if (mAlbumAPI.Status.IsSuccessfull == false)
                {
                    mStatus = mAlbumAPI.Status;
                }

                if (mStatus.IsSuccessfull == true)
                {
                    //Restart the PhotoAlbumHistory

                    //Add to collection for next time
                    AddPhotoListToAlbumHistory(ref photos);
                }
            }
            catch (Exception ex)
            {
                mStatus.IsSuccessfull = false;
                mStatus.AddNewErrorMessage(source, ex.Message);
            }
            
            return PhotoAlbumHistory;
        }

        internal List<Album> GetAllAlbums()
        {
            const string source = mSource + "GetAllAlbums";

            List<Album> albums = new List<Album>();

            try
            {
                albums = mAlbumAPI.GetAllAlbums();

                if (mAlbumAPI.Status.IsSuccessfull == false)
                {
                    mStatus = mAlbumAPI.Status;
                }

                if (mStatus.IsSuccessfull == true)
                {
                    //Restart the PhotoAlbumHistory

                    //Add to collection for next time
                    foreach (Album album in albums)
                    {
                        if(PhotoAlbumHistory.ContainsKey(album.Id) == false)
                        {
                            //does not exist already add it
                            PhotoAlbumHistory.Add(album.Id, new Dictionary<int, Photo>());
                        }
                    }
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

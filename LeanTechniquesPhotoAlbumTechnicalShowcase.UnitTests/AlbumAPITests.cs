using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LeanTechniquesPhotoAlbumTechnicalShowcase.Services;
using System.Collections.Generic;

namespace LeanTechniquesPhotoAlbumTechnicalShowcase.UnitTests
{
    [TestClass]
    public class AlbumAPITests
    {
        [TestMethod]
        public void GetAllPhotosByAlbumID_IDExists_ExpectTrue()
        {
            AlbumAPI albumAPI = new AlbumAPI();
            List<Models.Photo> photos = albumAPI.GetPhotosByAlbumID(1);


            Assert.IsTrue(albumAPI.Status.IsSuccessfull);
            Assert.IsNotNull(photos);
            Assert.IsTrue(photos.Count == 50);

        }

        [TestMethod]
        public void GetAllPhotosByAlbumID_IDExists_ExpectFalse()
        {
            AlbumAPI albumAPI = new AlbumAPI();
            List<Models.Photo> photos = albumAPI.GetPhotosByAlbumID(-1);

            Assert.IsTrue(albumAPI.Status.IsSuccessfull);
            Assert.IsNotNull(photos);
            Assert.IsFalse(photos.Count > 0);

        }

        [TestMethod]
        public void GetAllPhotos_PhotosCount_ExpectTrue()
        {
            AlbumAPI albumAPI = new AlbumAPI();
            List<Models.Photo> photos = albumAPI.GetAllPhotos();

            Assert.IsTrue(albumAPI.Status.IsSuccessfull);
            Assert.IsNotNull(photos);
            Assert.IsTrue(photos.Count == 5000);

        }

        [TestMethod]
        public void GetAllAlbums_AlbumCount_ExpectTrue()
        {
            AlbumAPI albumAPI = new AlbumAPI();
            List<Models.Album> albums = albumAPI.GetAllAlbums();

            Assert.IsTrue(albumAPI.Status.IsSuccessfull);
            Assert.IsNotNull(albums);
            Assert.IsTrue(albums.Count == 100);

        }
    }
}

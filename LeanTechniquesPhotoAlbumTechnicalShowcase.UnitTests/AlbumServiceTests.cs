using LeanTechniquesPhotoAlbumTechnicalShowcase.Models;
using LeanTechniquesPhotoAlbumTechnicalShowcase.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LeanTechniquesPhotoAlbumTechnicalShowcase.UnitTests
{
    [TestClass]
    public class AlbumServiceTests
    {
        [TestMethod]
        public void GetAllAlbums_PhotoHistoryExists_ExpectTrue()
        {
            AlbumService albumService = new AlbumService();

            albumService.GetAllAlbums();

            Assert.IsTrue(albumService.Status.IsSuccessfull);
            Assert.IsTrue(albumService.PhotoAlbumHistory.ContainsKey(1));

        }

        [TestMethod]
        public void GetAllAlbums_PhotoHistoryExists_ExpectFalse()
        {
            AlbumService albumService = new AlbumService();

            albumService.GetAllAlbums();

            Assert.IsTrue(albumService.Status.IsSuccessfull);
            Assert.IsFalse(albumService.PhotoAlbumHistory.ContainsKey(-1));

        }
        [TestMethod]
        public void GetAllAlbums_AlbumCount_ExpectTrue()
        {
            AlbumService albumService = new AlbumService();
            List<Album> albumList = new List<Album>();

            albumList = albumService.GetAllAlbums();


            Assert.IsTrue(albumService.Status.IsSuccessfull);
            Assert.IsTrue(albumList.Count ==100);

        }

        [TestMethod]
        public void GetAllPhotos_PhotoHistoryExists_ExpectTrue()
        {
            AlbumService albumService = new AlbumService();

            albumService.GetAllAlbumsAndPhotos();

            Assert.IsTrue(albumService.Status.IsSuccessfull);
            Assert.IsTrue(albumService.PhotoAlbumHistory.ContainsKey(1));
            Assert.IsTrue(albumService.PhotoAlbumHistory[1].ContainsKey(1));

        }

        [TestMethod]
        public void GetAllPhotos_PhotoHistoryExists_ExpectFalse()
        {
            AlbumService albumService = new AlbumService();

            albumService.GetAllAlbumsAndPhotos();


            Assert.IsTrue(albumService.Status.IsSuccessfull);
            Assert.IsTrue(albumService.PhotoAlbumHistory.ContainsKey(1));
            Assert.IsFalse(albumService.PhotoAlbumHistory[1].ContainsKey(1000));

        }
        [TestMethod]
        public void GetAllPhotos_PhotoExists_ExpectTrue()
        {
            AlbumService albumService = new AlbumService();
            Dictionary<int, Dictionary<int, Photo>> albumList = new Dictionary<int, Dictionary<int, Photo>>();

            albumList = albumService.GetAllAlbumsAndPhotos();


            Assert.IsTrue(albumService.Status.IsSuccessfull);
            Assert.IsTrue(albumList.Count == 100);
            Assert.IsTrue(albumList.ContainsKey(1));
            Assert.IsTrue(albumList[1].ContainsKey(1));

        }
        [TestMethod]
        public void GetAllPhotos_PhotoExists_ExpectFalse()
        {
            AlbumService albumService = new AlbumService();
            Dictionary<int, Dictionary<int, Photo>> albumList = new Dictionary<int, Dictionary<int, Photo>>();

            albumList = albumService.GetAllAlbumsAndPhotos();


            Assert.IsTrue(albumService.Status.IsSuccessfull);
            Assert.IsTrue(albumList.Count == 100);
            Assert.IsTrue(albumList.ContainsKey(1));
            Assert.IsFalse(albumList[1].ContainsKey(1000));

        }
        [TestMethod]
        public void GetAllPhotos_PhotoCount_ExpectTrue()
        {
            AlbumService albumService = new AlbumService();
            Dictionary<int, Dictionary<int, Photo>> albumList = new Dictionary<int, Dictionary<int, Photo>>();

            albumList = albumService.GetAllAlbumsAndPhotos();


            Assert.IsTrue(albumService.Status.IsSuccessfull);
            Assert.IsTrue(albumList.Count == 100);
            Assert.IsTrue(albumList.ContainsKey(1));
            Assert.IsTrue(albumList[1].Count == 50);

        }
        [TestMethod]
        public void GetPhotosForAlbum_PhotoCount_ExpectTrue()
        {
            AlbumService albumService = new AlbumService();
            List<Photo> photos = new List<Photo>();

            photos = albumService.GetPhotosForAlbum(1);


            Assert.IsTrue(albumService.Status.IsSuccessfull);
            Assert.IsTrue(photos.Count == 50);
        }
        [TestMethod]
        public void GetPhotosForAlbum_PhotoAlbumExists_ExpectFalse()
        {
            AlbumService albumService = new AlbumService();
            List<Photo> photos = new List<Photo>();

            photos = albumService.GetPhotosForAlbum(1000);


            Assert.IsTrue(albumService.Status.IsSuccessfull);
            Assert.IsTrue(photos.Count == 0);
        }
    }
}

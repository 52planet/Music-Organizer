// **************************************************
// WEB524 Project Template V2 == 0b6fd920-b1be-41a2-931b-71e9af8872c5
// Do not change this header.
// **************************************************

using AutoMapper;
using F2021A5LB.EntityModels;
using F2021A5LB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace F2021A5LB.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // AutoMapper instance
        public IMapper mapper;

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }

        // Default constructor...
        public Manager()
        {
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();
                //Albums
                cfg.CreateMap<AlbumAddViewModel, Album>();
                cfg.CreateMap<Album, AlbumBaseViewModel>();
                cfg.CreateMap<Album, AlbumWithDetailViewModel>();
                cfg.CreateMap<AlbumBaseViewModel, AlbumAddFormViewModel>();
                cfg.CreateMap<AlbumAddFormViewModel, AlbumAddViewModel>();
                //Artists
                cfg.CreateMap<Artist, ArtistBaseViewModel>();
                cfg.CreateMap<Artist, ArtistWithDetailViewModel>();
                cfg.CreateMap<ArtistBaseViewModel, ArtistAddFormViewModel>();
                cfg.CreateMap< ArtistAddFormViewModel, ArtistAddViewModel>();
                cfg.CreateMap<ArtistAddViewModel, Artist>();
                //Tracks

                cfg.CreateMap<Track, TrackWithDetailViewModel>();
                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<TrackBaseViewModel, TrackAddFormViewModel>();
                cfg.CreateMap<TrackAddFormViewModel, TrackAddViewModel>();
                cfg.CreateMap<TrackAddViewModel, Track>();
                //Genre
                cfg.CreateMap<Genre, GenreBaseViewModel>();

            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // ############################################################
        // RoleClaim

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()

        //Genre
        public IEnumerable<GenreBaseViewModel> GenreGetAll()
        {
            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreBaseViewModel>>(ds.Genres.OrderBy(g => g.Name));
        }

        //Artist
        public IEnumerable<ArtistBaseViewModel> ArtistGetAll()
        {
            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBaseViewModel>>(ds.Artists.OrderBy(a => a.Name));
        }

        public ArtistWithDetailViewModel ArtistGetOne(int id_)
        {
            return mapper.Map<Artist, ArtistWithDetailViewModel>(ds.Artists.Include("Albums").SingleOrDefault(a => a.Id == id_));
        }

        public ArtistWithDetailViewModel ArtistAddNew(ArtistAddViewModel model)
        {
            var addedItem = ds.Artists.Add(mapper.Map<ArtistAddViewModel, Artist>(model));
           
            addedItem.Executive = HttpContext.Current.User.Identity.Name;
            if (addedItem.Executive != null)
            {
                ds.SaveChanges();
                return (addedItem == null) ? null : mapper.Map<Artist, ArtistWithDetailViewModel>(addedItem);
            }
            else
            {
                return null;
            }
            
        }

        //Album
        public IEnumerable<AlbumBaseViewModel> AlbumGetAll()
        {
            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBaseViewModel>>(ds.Albums.OrderBy(a => a.ReleaseDate));
        }

        public AlbumWithDetailViewModel AlbumGetOne(int? id_)
        {
            return mapper.Map<Album, AlbumWithDetailViewModel>(ds.Albums.Include("Artists").Include("Tracks").SingleOrDefault(a => a.Id == id_));
        }

        public AlbumWithDetailViewModel AlbumAddNew(AlbumAddViewModel newItem)
        {
            var addedItem = ds.Albums.Add(mapper.Map<AlbumAddViewModel, Album>(newItem));
            addedItem.Artists = new List<Artist>();
            addedItem.Tracks = new List<Track>();
            //validate Artists

            bool test = true;
            foreach (var item in newItem.ArtistIds)
            {
                var a = ds.Artists.Find(item);
                if (a == null)
                {
                    test = false;
                }
                else
                {
                    addedItem.Artists.Add(a);
                }
            }
            //Validate Tracks

            if (newItem.TrackIds != null)
            {
                foreach (var item in newItem.TrackIds)
                {
                    var t = ds.Tracks.Find(item);
                    if (t == null)
                    {
                        test = false;
                    }
                    else
                    {
                        addedItem.Tracks.Add(t);
                    }
                }
            }
            else
            {
                test = true;
            }


            if (test == true)
            {
                addedItem.Coordinator = HttpContext.Current.User.Identity.Name;
                
                
                ds.SaveChanges();
                return (addedItem == null) ? null : mapper.Map<Album, AlbumWithDetailViewModel>(addedItem);
            }
            else 
            {
                return null;
            }

        }

        //Tracks
        public IEnumerable<TrackBaseViewModel> TrackGetAll()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(ds.Tracks.OrderBy(t => t.Name));
        }

        public TrackWithDetailViewModel TrackGetOne(int id_)
        {
            var o = ds.Tracks.Include("Albums").SingleOrDefault(t => t.Id == id_);

            if (o == null)
            {
                return null;
            }
            else 
            {
                var result = mapper.Map<Track, TrackWithDetailViewModel>(o);
                result.AlbumNames = o.Albums.Select(a => a.Name);
                return result;
            }
        }

        public TrackWithDetailViewModel TrackAddNew(TrackAddViewModel model)
        {
            var album_ = ds.Albums.Find(model.AlbumId);
            if (album_ != null)
            {
                var addedItem = ds.Tracks.Add(mapper.Map<TrackAddViewModel, Track>(model));
                addedItem.Clerk = HttpContext.Current.User.Identity.Name;
                addedItem.Albums = new List<Album>();
                addedItem.Albums.Add(album_);
                ds.SaveChanges();
                return (addedItem == null) ? null : mapper.Map<Track, TrackWithDetailViewModel>(addedItem);
            }
            else 
            {
                return null;
            }
            
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAllByArtistId(int id)
        {
            var o = ds.Artists.Include("Albums.Tracks").SingleOrDefault(a => a.Id == id);

            if (o == null)
            {
                return null;
            }

            var c = new List<Track>();

            foreach (var album in o.Albums)
            {
                c.AddRange(album.Tracks);
            }

            c = c.Distinct().ToList();

            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(c.OrderBy(t => t.Name));
        }

        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        public bool LoadGenres()
        {
            // Return if there's existing data
            if (ds.Genres.Count() > 0) { return false; }

            ds.Genres.Add(new Genre {  Id = 1,Name = "Rock"});
            ds.Genres.Add(new Genre {  Name = "Metal" });
            ds.Genres.Add(new Genre {  Name = "Pop" });
            ds.Genres.Add(new Genre {  Name = "Rap" });
            ds.Genres.Add(new Genre {  Name = "Disco" });
            ds.Genres.Add(new Genre {  Name = "RnB" });
            ds.Genres.Add(new Genre {  Name = "EDM" });
            ds.Genres.Add(new Genre {  Name = "Soul" });
            ds.Genres.Add(new Genre {  Name = "Jazz" });
            ds.Genres.Add(new Genre {  Name = "Blues" });
            // Save changes
            ds.SaveChanges();
            return true;
        }
        public bool LoadArtists()
        {
            // Return if there's existing data
            if (ds.Artists.Count() > 0) { return false; }

            // User name
            var user = HttpContext.Current.User.Identity.Name;
            //p!ink
            ds.Artists.Add(new Artist { BirthName = "Aubrey Drake Graham", Executive = user, Genre = "Rap", Name = "Drake", BirthOrStartDate = DateTime.Parse("1986-10-24"), UrlArtist = "https://akns-images.eonline.com/eol_images/Entire_Site/2020912/rs_1200x1200-201012052848-1200-Drake-Euphoria-Aubrey-Drake-Graham-ch-101220.jpg?fit=around%7C1200:1200&output-quality=90&crop=1200:1200;center,top" });
            //Architects
            ds.Artists.Add(new Artist { Executive = user, Genre = "Metal", Name = "Architects", BirthOrStartDate = DateTime.Parse("2004-01-01"), UrlArtist = "https://www.revolvermag.com/sites/default/files/media/images/article/architects-web-crop-ed-mason-1.jpg", });
            //Guthrie
            ds.Artists.Add(new Artist { BirthName = "Plini Roessler-Holgate", Executive = user, Genre = "Rock", Name = "Plini", BirthOrStartDate = DateTime.Parse("1992-06-22"), UrlArtist = "https://lastfm.freetls.fastly.net/i/u/770x0/171b1dbf7af84a3eca89175d53b4948c.jpg", });
            // Save changes
            ds.SaveChanges();
            return true;
        }
            
           
           
            
        public bool LoadAlbums()
        {
            // Return if there's existing data
            if (ds.Albums.Count() > 0) { return false; }
            // User name
            var user = HttpContext.Current.User.Identity.Name;
            //select artist
            var artist = ds.Artists.SingleOrDefault(a => a.Name == "Plini");
            //
            ds.Albums.Add(new Album
            {
                Artists = new List<Artist> { artist },
                Name = "Impulse Voices",
                Coordinator = user,
                Genre = "Rock",
                ReleaseDate = DateTime.Parse("2020-09-23"),
                UrlAlbum = "https://f4.bcbits.com/img/a3947415487_10.jpg",
            });
            ds.Albums.Add(new Album
            {
                Artists = new List<Artist> { artist },
                Name = "Handmade Cities",
                Coordinator = user,
                Genre = "Rock",
                ReleaseDate = DateTime.Parse("2016-08-26"),
                UrlAlbum = "https://f4.bcbits.com/img/a2040672325_10.jpg",

            });
            //
            ds.SaveChanges();
            return true;
        }
        public bool LoadTracks()
        {
            // Return if there's existing data
            if (ds.Tracks.Count() > 0) { return false; }
            // User name
            var user = HttpContext.Current.User.Identity.Name;
            //select artist
            var Impulse_Voices = ds.Albums.SingleOrDefault(a => a.Name == "Impulse Voices");
            var Handmade_Cities = ds.Albums.SingleOrDefault(a => a.Name == "Handmade Cities");
            //Impulse voices
            ds.Tracks.Add(new Track 
            {
                Albums = new List<Album> { Impulse_Voices },
                Clerk = user,
                Genre = "Rock",
                Composers = "Plini",
                Name = "I'll Tell You Someday"
            });
            ds.Tracks.Add(new Track
            {
                Albums = new List<Album> { Impulse_Voices },
                Clerk = user,
                Genre = "Rock",
                Composers = "Plini",
                Name = "Last Call"
            });
            ds.Tracks.Add(new Track
            {
                Albums = new List<Album> { Impulse_Voices },
                Clerk = user,
                Genre = "Rock",
                Composers = "Plini",
                Name = "Papelillo"
            });
            ds.Tracks.Add(new Track
            {
                Albums = new List<Album> { Impulse_Voices },
                Clerk = user,
                Genre = "Rock",
                Composers = "Plini",
                Name = "Perfume"
            });
            ds.Tracks.Add(new Track
            {
                Albums = new List<Album> { Impulse_Voices },
                Clerk = user,
                Genre = "Rock",
                Composers = "Plini",
                Name = "Pan"
            });
            //Handmade cities
            ds.Tracks.Add(new Track
            {
                Albums = new List<Album> { Handmade_Cities },
                Clerk = user,
                Genre = "Rock",
                Composers = "Plini",
                Name = "Inhale"
              
            });
            ds.Tracks.Add(new Track
            {
                Albums = new List<Album> { Handmade_Cities },
                Clerk = user,
                Genre = "Rock",
                Composers = "Plini",
                Name = "Electric Sunrise"
            });
            ds.Tracks.Add(new Track
            {
                Albums = new List<Album> { Handmade_Cities },
                Clerk = user,
                Genre = "Rock",
                Composers = "Plini",
                Name = "Cascade"
            });
            ds.Tracks.Add(new Track
            {
                Albums = new List<Album> { Handmade_Cities },
                Clerk = user,
                Genre = "Rock",
                Composers = "Plini",
                Name = "Pastures"
            });
            ds.Tracks.Add(new Track
            {
                Albums = new List<Album> { Handmade_Cities },
                Clerk = user,
                Genre = "Rock",
                Composers = "Plini",
                Name = "Every Piece Matters"
            });


            ds.SaveChanges();
            return true;
        }

        //Remove data
        public bool RemoveTracks()
        {
            //TODO:
            return true;
        }


        public bool LoadData()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // ############################################################
            // Role claims

            if (ds.RoleClaims.Count() == 0)
            {
                // Add role claims here
                ds.RoleClaims.Add(new RoleClaim { Name = "Executive" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Coordinator" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Clerk" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Staff" });
                ds.SaveChanges();
                done = true;
            }

            LoadGenres();
            LoadArtists();
            LoadAlbums();
            LoadTracks();
            //done = RemoveDatabase();



            return done;
        }

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    // New "RequestUser" class for the authenticated user
    // Includes many convenient members to make it easier to render user account info
    // Study the properties and methods, and think about how you could use it

    // How to use...

    // In the Manager class, declare a new property named User
    //public RequestUser User { get; private set; }

    // Then in the constructor of the Manager class, initialize its value
    //User = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);

    public class RequestUser
    {
        // Constructor, pass in the security principal
        public RequestUser(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                // You can change the string value in your app to match your app domain logic
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            // Compose the nicely-formatted full names
            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }

}
namespace AracTakip.Services;

public class FirebaseService
{
    private readonly string _apiKey;
    private readonly string _storageBucket;
    private readonly string _firestoreProjectId;

    public FirebaseService(string apiKey, string storageBucket, string firestoreProjectId)
    {
        _apiKey = apiKey;
        _storageBucket = storageBucket;
        _firestoreProjectId = firestoreProjectId;
    }

    // Buraya daha sonra UploadFileAsync() vs. gelecek
}

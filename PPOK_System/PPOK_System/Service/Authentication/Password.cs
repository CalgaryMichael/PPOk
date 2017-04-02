namespace PPOK_System.Service.Authentication {
	public class Password {
		public static bool Authenticate(string attempt, string actual) {
			var encoded = SHA1.Encode(attempt);
			return encoded == actual ? true : false;
			//if (encoded == actual)
			//	return true;
			//return false;
		}
	}
}
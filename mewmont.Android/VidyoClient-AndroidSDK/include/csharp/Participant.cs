// DO NOT EDIT! This is an autogenerated file. All changes will be
// overwritten!

//	Copyright (c) 2016 Vidyo, Inc. All rights reserved.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Runtime.InteropServices;

namespace VidyoClient
{
	public class Participant{
#if __IOS__
		const string importLib = "__Internal";
#else
		const string importLib = "libVidyoClient";
#endif
		private IntPtr objPtr; // opaque VidyoParticipant reference.
		public IntPtr GetObjectPtr(){
			return objPtr;
		}
		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoParticipantConstructCopyNative(IntPtr other);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoParticipantDestructNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoParticipantGetContactNative(IntPtr p, IntPtr contact);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoParticipantGetIdNative(IntPtr p);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoParticipantGetNameNative(IntPtr p);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I4)]
		private static extern ParticipantTrust VidyoParticipantGetTrustNative(IntPtr p);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoParticipantGetUserIdNative(IntPtr p);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern Boolean VidyoParticipantIsHiddenNative(IntPtr p);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern Boolean VidyoParticipantIsLocalNative(IntPtr p);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern Boolean VidyoParticipantIsRecordingNative(IntPtr p);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern Boolean VidyoParticipantIsSelectableNative(IntPtr p);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr VidyoParticipantGetUserDataNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		public static extern void VidyoParticipantSetUserDataNative(IntPtr obj, IntPtr userData);

		public enum ParticipantTrust{
			ParticipanttrustLocal,
			ParticipanttrustFederated,
			ParticipanttrustAnonymous
		}
		public Participant(IntPtr other){
			objPtr = VidyoParticipantConstructCopyNative(other);
			VidyoParticipantSetUserDataNative(objPtr, GCHandle.ToIntPtr(GCHandle.Alloc(this, GCHandleType.Weak)));
		}
		~Participant(){
			if(objPtr != IntPtr.Zero){
				VidyoParticipantSetUserDataNative(objPtr, IntPtr.Zero);
				VidyoParticipantDestructNative(objPtr);
			}
		}
		public Contact GetContact(Contact contact){

			IntPtr ret = VidyoParticipantGetContactNative(objPtr, (contact != null) ? contact.GetObjectPtr():IntPtr.Zero);
			Contact csRet = new Contact(ret);

			return csRet;
		}
		public String GetId(){

			IntPtr ret = VidyoParticipantGetIdNative(objPtr);

			return Marshal.PtrToStringAnsi(ret);
		}
		public String GetName(){

			IntPtr ret = VidyoParticipantGetNameNative(objPtr);

			return Marshal.PtrToStringAnsi(ret);
		}
		public ParticipantTrust GetTrust(){

			ParticipantTrust ret = VidyoParticipantGetTrustNative(objPtr);

			return ret;
		}
		public String GetUserId(){

			IntPtr ret = VidyoParticipantGetUserIdNative(objPtr);

			return Marshal.PtrToStringAnsi(ret);
		}
		public Boolean IsHidden(){

			Boolean ret = VidyoParticipantIsHiddenNative(objPtr);

			return ret;
		}
		public Boolean IsLocal(){

			Boolean ret = VidyoParticipantIsLocalNative(objPtr);

			return ret;
		}
		public Boolean IsRecording(){

			Boolean ret = VidyoParticipantIsRecordingNative(objPtr);

			return ret;
		}
		public Boolean IsSelectable(){

			Boolean ret = VidyoParticipantIsSelectableNative(objPtr);

			return ret;
		}
	};
}

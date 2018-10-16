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
	public class RemoteMicrophone{
#if __IOS__
		const string importLib = "__Internal";
#else
		const string importLib = "libVidyoClient";
#endif
		private IntPtr objPtr; // opaque VidyoRemoteMicrophone reference.
		public IntPtr GetObjectPtr(){
			return objPtr;
		}
		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern Boolean VidyoRemoteMicrophoneAddToLocalSpeakerNative(IntPtr m, IntPtr speaker, [MarshalAs(UnmanagedType.I4)]RemoteMicrophoneMode mode);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoRemoteMicrophoneConstructCopyNative(IntPtr other);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoRemoteMicrophoneDestructNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoRemoteMicrophoneGetIdNative(IntPtr m);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoRemoteMicrophoneGetNameNative(IntPtr m);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I4)]
		private static extern Device.DeviceAudioSignalType VidyoRemoteMicrophoneGetSignalTypeNative(IntPtr m);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern Boolean VidyoRemoteMicrophoneRemoveFromLocalSpeakerNative(IntPtr m, IntPtr speaker);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr VidyoRemoteMicrophoneGetUserDataNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		public static extern void VidyoRemoteMicrophoneSetUserDataNative(IntPtr obj, IntPtr userData);

		public enum RemoteMicrophoneMode{
			RemotemicrophonemodeDynamic,
			RemotemicrophonemodeStatic
		}
		public RemoteMicrophone(IntPtr other){
			objPtr = VidyoRemoteMicrophoneConstructCopyNative(other);
			VidyoRemoteMicrophoneSetUserDataNative(objPtr, GCHandle.ToIntPtr(GCHandle.Alloc(this, GCHandleType.Weak)));
		}
		~RemoteMicrophone(){
			if(objPtr != IntPtr.Zero){
				VidyoRemoteMicrophoneSetUserDataNative(objPtr, IntPtr.Zero);
				VidyoRemoteMicrophoneDestructNative(objPtr);
			}
		}
		public Boolean AddToLocalSpeaker(LocalSpeaker speaker, RemoteMicrophoneMode mode){

			Boolean ret = VidyoRemoteMicrophoneAddToLocalSpeakerNative(objPtr, (speaker != null) ? speaker.GetObjectPtr():IntPtr.Zero, mode);

			return ret;
		}
		public String GetId(){

			IntPtr ret = VidyoRemoteMicrophoneGetIdNative(objPtr);

			return Marshal.PtrToStringAnsi(ret);
		}
		public String GetName(){

			IntPtr ret = VidyoRemoteMicrophoneGetNameNative(objPtr);

			return Marshal.PtrToStringAnsi(ret);
		}
		public Device.DeviceAudioSignalType GetSignalType(){

			Device.DeviceAudioSignalType ret = VidyoRemoteMicrophoneGetSignalTypeNative(objPtr);

			return ret;
		}
		public Boolean RemoveFromLocalSpeaker(LocalSpeaker speaker){

			Boolean ret = VidyoRemoteMicrophoneRemoveFromLocalSpeakerNative(objPtr, (speaker != null) ? speaker.GetObjectPtr():IntPtr.Zero);

			return ret;
		}
	};
}

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
	public class FormField{
#if __IOS__
		const string importLib = "__Internal";
#else
		const string importLib = "libVidyoClient";
#endif
		private IntPtr objPtr; // opaque VidyoFormField reference.
		public IntPtr GetObjectPtr(){
			return objPtr;
		}
		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern uint VidyoFormFieldGetcolsNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern uint VidyoFormFieldGetmaxlengthNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern Boolean VidyoFormFieldGetmultipleNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoFormFieldGetnameNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoFormFieldGetoptionsNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoFormFieldGetoptionsArrayNative(IntPtr obj, ref int size);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoFormFieldFreeoptionsArrayNative(IntPtr obj, int size);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern uint VidyoFormFieldGetrowsNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern uint VidyoFormFieldGetsizeNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I4)]
		private static extern FormFieldType VidyoFormFieldGettypeNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoFormFieldGetvalueNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I4)]
		private static extern FormFieldTextWrapMode VidyoFormFieldGetwrapNative(IntPtr obj);

		public uint cols;
		public uint maxlength;
		public Boolean multiple;
		public String name;
		public List<FormFieldOption> options;
		public uint rows;
		public uint size;
		public FormFieldType type;
		public String value;
		public FormFieldTextWrapMode wrap;
		public FormField(IntPtr obj){
			objPtr = obj;

			List<FormFieldOption> csOptions = new List<FormFieldOption>();
			int nOptionsSize = 0;
			IntPtr nOptions = VidyoFormFieldGetoptionsArrayNative(VidyoFormFieldGetoptionsNative(objPtr), ref nOptionsSize);
			int nOptionsIndex = 0;
			while (nOptionsIndex < nOptionsSize) {
				FormFieldOption csToptions = new FormFieldOption(Marshal.ReadIntPtr(nOptions + (nOptionsIndex * Marshal.SizeOf(nOptions))));
				csOptions.Add(csToptions);
				nOptionsIndex++;
			}

			cols = VidyoFormFieldGetcolsNative(objPtr);
			maxlength = VidyoFormFieldGetmaxlengthNative(objPtr);
			multiple = VidyoFormFieldGetmultipleNative(objPtr);
			name = Marshal.PtrToStringAnsi(VidyoFormFieldGetnameNative(objPtr));
			options = csOptions;
			rows = VidyoFormFieldGetrowsNative(objPtr);
			size = VidyoFormFieldGetsizeNative(objPtr);
			type = VidyoFormFieldGettypeNative(objPtr);
			value = Marshal.PtrToStringAnsi(VidyoFormFieldGetvalueNative(objPtr));
			wrap = VidyoFormFieldGetwrapNative(objPtr);
			VidyoFormFieldFreeoptionsArrayNative(nOptions, nOptionsSize);
		}
	};
}

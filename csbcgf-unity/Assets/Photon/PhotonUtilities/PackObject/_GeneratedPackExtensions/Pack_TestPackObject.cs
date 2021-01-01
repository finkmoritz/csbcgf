using System;
using System.Reflection;
using System.Collections;
using Photon.Utilities;
using UnityEngine;
#pragma warning disable 1635
#pragma warning disable 0219
namespace Photon.Compression.Internal
{
	public class PackFrame_TestPackObject : PackFrame
	{
		public System.Single rotation;
		public System.Int32 intoroboto;

		public static void Interpolate(PackFrame start, PackFrame end, PackFrame trg, float time, ref FastBitMask128 readyMask, ref int maskOffset)
		{
			var s = start as PackFrame_TestPackObject;
			var e = end as PackFrame_TestPackObject;
			var t = trg as PackFrame_TestPackObject;
			var mask = end.mask;
			if (mask[maskOffset]){ t.rotation = (System.Single)((e.rotation - s.rotation) * time) + s.rotation;} maskOffset++;
			if (mask[maskOffset]){ t.intoroboto = (System.Int32)((e.intoroboto - s.intoroboto) * time) + s.intoroboto;} maskOffset++;
		}
		public static void Interpolate(PackFrame start, PackFrame end, System.Object trg, float time, ref FastBitMask128 readyMask, ref int maskOffset)
		{
			var s = start as PackFrame_TestPackObject;
			var e = end as PackFrame_TestPackObject;
			var t = trg as emotitron.TestPackObject;
			var mask = end.mask;
			if (readyMask[maskOffset] && mask[maskOffset]){ t.rotation = (System.Single)((e.rotation - s.rotation) * time) + s.rotation; } maskOffset++;
			maskOffset++;
		}
		public static void SnapshotCallback(PackFrame snapframe, PackFrame targframe, System.Object trg, ref FastBitMask128 readyMask, ref int maskOffset)
		{
			var snap = snapframe as PackFrame_TestPackObject;
			var targ = targframe as PackFrame_TestPackObject;
			var t = trg as emotitron.TestPackObject;
			var snapmask = snapframe.mask;
			var targmask = targframe.mask;
			if(readyMask[maskOffset]) { var snapval = snapmask[maskOffset] ? snap.rotation : t.rotation; var targval = targmask[maskOffset] ? targ.rotation : snapval; t.SnapshotHook(snapval,  targval); } maskOffset++;
			maskOffset++;
		}
		public static void Capture(System.Object src, PackFrame trg)
		{
			var s = src as emotitron.TestPackObject;
			var t = trg as PackFrame_TestPackObject;
			t.rotation = s.rotation; 
			t.intoroboto = s.intoroboto; 
		}
		public static void Apply(PackFrame src, System.Object trg, ref FastBitMask128 mask, ref int maskOffset)
		{
			var s = src as PackFrame_TestPackObject;
			var t = trg as emotitron.TestPackObject;
			{ if (mask[maskOffset]){ var hold = t.rotation; t.rotation = s.rotation;t.RotationHook(s.rotation, hold); } } maskOffset++;
			{ if (mask[maskOffset]){ t.intoroboto = s.intoroboto; } } maskOffset++;
		}
		public static void Copy(PackFrame src, PackFrame trg)
		{
			var s = src as PackFrame_TestPackObject;
			var t = trg as PackFrame_TestPackObject;
			t.rotation = s.rotation;
			t.intoroboto = s.intoroboto;
		}

		public static PackFrame Factory() { return new PackFrame_TestPackObject(){ mask = new FastBitMask128(Pack_TestPackObject.TOTAL_FIELDS), isCompleteMask = new FastBitMask128(Pack_TestPackObject.TOTAL_FIELDS) }; }
		public static PackFrame[] Factory(System.Object trg, int count){ var t = trg as emotitron.TestPackObject;var frames = new PackFrame_TestPackObject[count]; for (int i = 0; i < count; ++i) { var frame = new PackFrame_TestPackObject(){ mask = new FastBitMask128(Pack_TestPackObject.TOTAL_FIELDS), isCompleteMask = new FastBitMask128(Pack_TestPackObject.TOTAL_FIELDS) };  frames[i] = frame; } return frames; }
	}

	public static class Pack_TestPackObject
	{
		public const int LOCAL_FIELDS = 2;

		public const int TOTAL_FIELDS = 2;

		public static PackObjectDatabase.PackObjectInfo packObjInfo;
		static PackDelegate<Single> rotationPacker;
		static UnpackDelegate<Single> rotationUnpacker;

		static PackDelegate<Int32> intorobotoPacker;
		static UnpackDelegate<Int32> intorobotoUnpacker;

		public static bool initialized;
		public static bool isInitializing;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void Initialize()
		{
			if (initialized) return;
			isInitializing = true;
			int maxBits = 0;
			var pObjAttr = (typeof(emotitron.TestPackObject).GetCustomAttributes(typeof(PackObjectAttribute), false)[0] as PackObjectAttribute);
			var defaultKeyRate = pObjAttr.defaultKeyRate;
			FastBitMask128 defReadyMask = new FastBitMask128(TOTAL_FIELDS);
			int fieldindex = 0;

			SyncHalfFloatAttribute rotationPackAttr = (SyncHalfFloatAttribute)(typeof(emotitron.TestPackObject).GetField("rotation").GetCustomAttributes(typeof(SyncVarBaseAttribute), false)[0] as SyncHalfFloatAttribute);
			rotationPacker = (rotationPackAttr as IPackSingle).Pack;
			rotationUnpacker = (rotationPackAttr as IPackSingle).Unpack;
			rotationPackAttr.Initialize(typeof(System.Single));
			if (rotationPackAttr.keyRate == KeyRate.UseDefault) rotationPackAttr.keyRate = (KeyRate)defaultKeyRate;
			if (rotationPackAttr.syncAs == SyncAs.Auto) rotationPackAttr.syncAs = pObjAttr.syncAs;
			if (rotationPackAttr.syncAs == SyncAs.Auto) rotationPackAttr.syncAs = SyncAs.State;
			if (rotationPackAttr.syncAs == SyncAs.Trigger) defReadyMask[fieldindex] = true;
			maxBits += 16; fieldindex++;

			SyncRangedIntAttribute intorobotoPackAttr = (SyncRangedIntAttribute)(typeof(emotitron.TestPackObject).GetField("intoroboto").GetCustomAttributes(typeof(SyncVarBaseAttribute), false)[0] as SyncRangedIntAttribute);
			intorobotoPacker = (intorobotoPackAttr as IPackInt32).Pack;
			intorobotoUnpacker = (intorobotoPackAttr as IPackInt32).Unpack;
			intorobotoPackAttr.Initialize(typeof(System.Int32));
			if (intorobotoPackAttr.keyRate == KeyRate.UseDefault) intorobotoPackAttr.keyRate = (KeyRate)defaultKeyRate;
			if (intorobotoPackAttr.syncAs == SyncAs.Auto) intorobotoPackAttr.syncAs = pObjAttr.syncAs;
			if (intorobotoPackAttr.syncAs == SyncAs.Auto) intorobotoPackAttr.syncAs = SyncAs.State;
			if (intorobotoPackAttr.syncAs == SyncAs.Trigger) defReadyMask[fieldindex] = true;
			maxBits += 2; fieldindex++;

			packObjInfo = new PackObjectDatabase.PackObjectInfo(defReadyMask, Pack, Pack, Unpack, maxBits, PackFrame_TestPackObject.Factory, PackFrame_TestPackObject.Factory, PackFrame_TestPackObject.Apply, PackFrame_TestPackObject.Capture, PackFrame_TestPackObject.Copy, PackFrame_TestPackObject.SnapshotCallback, PackFrame_TestPackObject.Interpolate, PackFrame_TestPackObject.Interpolate, TOTAL_FIELDS);
			PackObjectDatabase.packObjInfoLookup.Add(typeof(emotitron.TestPackObject), packObjInfo);
			isInitializing = false;
			initialized = true;
		}
		public static SerializationFlags Pack(ref System.Object obj, PackFrame prevFrame, ref FastBitMask128 mask, ref int maskOffset, byte[] buffer, ref int bitposition, int frameId, SerializationFlags writeFlags)
		{
			var packable = obj as emotitron.TestPackObject;
			var prev = prevFrame as PackFrame_TestPackObject;
			SerializationFlags flags = SerializationFlags.None;
			{
				var flag = rotationPacker(ref packable.rotation, prev.rotation, buffer, ref bitposition, frameId, writeFlags);
				mask[maskOffset] = flag != SerializationFlags.None;	flags |= flag; maskOffset++;
			}
			{
				var flag = intorobotoPacker(ref packable.intoroboto, prev.intoroboto, buffer, ref bitposition, frameId, writeFlags);
				mask[maskOffset] = flag != SerializationFlags.None;	flags |= flag; maskOffset++;
			}
			return flags;
		}
		public static SerializationFlags Pack(ref emotitron.TestPackObject packable, PackFrame prevFrame, ref FastBitMask128 mask, ref int maskOffset, byte[] buffer, ref int bitposition, int frameId, SerializationFlags writeFlags)
		{
			var prev = prevFrame as PackFrame_TestPackObject;
			SerializationFlags flags = SerializationFlags.None;
			{
				var flag = rotationPacker(ref packable.rotation, prev.rotation, buffer, ref bitposition, frameId, writeFlags);
				mask[maskOffset] = flag != SerializationFlags.None;	flags |= flag; maskOffset++;
			}
			{
				var flag = intorobotoPacker(ref packable.intoroboto, prev.intoroboto, buffer, ref bitposition, frameId, writeFlags);
				mask[maskOffset] = flag != SerializationFlags.None;	flags |= flag; maskOffset++;
			}
			return flags;
		}
		public static SerializationFlags Pack(PackFrame obj, PackFrame prevFrame, ref FastBitMask128 mask, ref int maskOffset, byte[] buffer, ref int bitposition, int frameId, SerializationFlags writeFlags)
		{
			var packable = obj as PackFrame_TestPackObject;
			var prev = prevFrame as PackFrame_TestPackObject;
			SerializationFlags flags = SerializationFlags.None;
			{
				var flag = rotationPacker(ref packable.rotation, prev.rotation, buffer, ref bitposition, frameId, writeFlags);
				mask[maskOffset] = flag != SerializationFlags.None;	flags |= flag; maskOffset++;
			}
			{
				var flag = intorobotoPacker(ref packable.intoroboto, prev.intoroboto, buffer, ref bitposition, frameId, writeFlags);
				mask[maskOffset] = flag != SerializationFlags.None;	flags |= flag; maskOffset++;
			}
			return flags;
		}
		public static SerializationFlags Unpack(PackFrame obj, ref FastBitMask128 mask, ref FastBitMask128 isCompleteMask, ref int maskOffset, byte[] buffer, ref int bitposition, int frameId, SerializationFlags writeFlags)
		{
			var packable = obj as PackFrame_TestPackObject;
			SerializationFlags flags = SerializationFlags.None;
			{
				if (mask[maskOffset]) {
					var flag = rotationUnpacker(ref packable.rotation, buffer, ref bitposition, frameId, writeFlags);
					isCompleteMask[maskOffset] = (flag & SerializationFlags.IsComplete) != 0; mask[maskOffset] = flag != 0; flags |= flag; 
				 } maskOffset++;
			}
			{
				if (mask[maskOffset]) {
					var flag = intorobotoUnpacker(ref packable.intoroboto, buffer, ref bitposition, frameId, writeFlags);
					isCompleteMask[maskOffset] = (flag & SerializationFlags.IsComplete) != 0; mask[maskOffset] = flag != 0; flags |= flag; 
				 } maskOffset++;
			}
			return flags;
		}
		public static SerializationFlags Pack(ref PackFrame_TestPackObject packable, PackFrame prevFrame, ref FastBitMask128 mask, ref int maskOffset, byte[] buffer, ref int bitposition, int frameId, SerializationFlags writeFlags)
		{
			var prev = prevFrame as PackFrame_TestPackObject;
			SerializationFlags flags = SerializationFlags.None;
			{
				var flag = rotationPacker(ref packable.rotation, prev.rotation, buffer, ref bitposition, frameId, writeFlags);
				mask[maskOffset] = flag != SerializationFlags.None;	flags |= flag; maskOffset++;
			}
			{
				var flag = intorobotoPacker(ref packable.intoroboto, prev.intoroboto, buffer, ref bitposition, frameId, writeFlags);
				mask[maskOffset] = flag != SerializationFlags.None;	flags |= flag; maskOffset++;
			}
			return flags;
		}
		public static SerializationFlags Unpack(ref PackFrame_TestPackObject packable, ref FastBitMask128 mask, ref FastBitMask128 isCompleteMask, ref int maskOffset, byte[] buffer, ref int bitposition, int frameId, SerializationFlags writeFlags)
		{
			SerializationFlags flags = SerializationFlags.None;
			{
				if (mask[maskOffset]) {
					var flag = rotationUnpacker(ref packable.rotation, buffer, ref bitposition, frameId, writeFlags);
					isCompleteMask[maskOffset] = (flag & SerializationFlags.IsComplete) != 0; mask[maskOffset] = flag != 0; flags |= flag; 
				 } maskOffset++;
			}
			{
				if (mask[maskOffset]) {
					var flag = intorobotoUnpacker(ref packable.intoroboto, buffer, ref bitposition, frameId, writeFlags);
					isCompleteMask[maskOffset] = (flag & SerializationFlags.IsComplete) != 0; mask[maskOffset] = flag != 0; flags |= flag; 
				 } maskOffset++;
			}
			return flags;
		}
	}
}
#pragma warning disable 0219
#pragma warning restore 1635

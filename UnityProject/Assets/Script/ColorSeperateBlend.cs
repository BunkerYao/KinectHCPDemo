using UnityEngine;
using System.Collections;

// -------------------------------------------------
// 类名：ColorSeperateBlend
// 描述：RGB颜色通道调节和1：1混合
// -------------------------------------------------
public class ColorSeperateBlend : ImageEffectBase {
	public float red = 1.0f;
	public float green = 1.0f;
	public float blue = 1.0f;
	// 是否将这个相机的渲染内容1：1混合到后缓冲
	public bool blendBackbuffer = false;

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetVector ("_colorFactors", new Vector4 (red, green, blue));
		Graphics.Blit (source, destination, material, blendBackbuffer ? 1 : 0);
	}
}

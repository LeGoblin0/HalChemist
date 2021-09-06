﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FunkyCode.Utilities;

namespace Rendering.Night {
	
    public static class LightSprite {

		public static class Script {
			
			static public void DrawScriptable(Scriptable.LightSprite2D id, Camera camera) {
				if (id.Sprite == null) {
					return;
				}

				if (id.InCamera(camera) == false) {
					return;
				}

				Vector2 position = id.Position;
				position.x -= camera.transform.position.x;
				position.y -= camera.transform.position.y;

				Vector2 scale = id.Scale;
				float rot = id.Rotation;

				Material material = Lighting2D.materials.GetAdditive();
				material.mainTexture = id.Sprite.texture;

				VirtualSpriteRenderer virtualSprite = new VirtualSpriteRenderer();
				virtualSprite.sprite = id.Sprite;

				GLExtended.color = new Color(id.Color.r * 0.5f, id.Color.g * 0.5f, id.Color.b * 0.5f, id.Color.a);

				material.SetPass(0);

				GL.Begin (GL.QUADS);

				Universal.Sprite.Pass.Draw(id.spriteMeshObject, virtualSprite, position, scale, rot);

				GL.End ();
				
				material.mainTexture = null;
			}
		}

		static public class Pass {
			public static Texture2D currentTexture;

			static public void Draw(LightSprite2D id, Camera camera) {
				if (id.GetSprite() == null) {
					return;
				}

				if (id.InCamera(camera) == false) {
					return;
				}

				Material material = Lighting2D.materials.GetLightSprite();

				if (material == null) {
					return;
				}

				Sprite sprite = id.GetSprite();
				if (sprite == null) {
					return;
				}

				Texture2D texture = sprite.texture;
				if (texture == null) {
					return;
				}

				if (texture != currentTexture) {
					if (currentTexture != null) {
						GL.End();
					}

					currentTexture = texture;
                    material.mainTexture = currentTexture;

                    material.SetPass(0);
                    GL.Begin(GL.QUADS);
				}
				
				Vector2 position = id.transform.position;
				position.x -= camera.transform.position.x;
				position.y -= camera.transform.position.y;

				Vector2 scale = id.transform.lossyScale;
				scale.x *= id.lightSpriteTransform.scale.x;
				scale.y *= id.lightSpriteTransform.scale.y;

				float rot = id.lightSpriteTransform.rotation;
				if (id.lightSpriteTransform.applyRotation) {
					rot += id.transform.rotation.eulerAngles.z;
				}
	
				float ratio = (float)texture.width / (float)texture.height;
				float type = id.type == LightSprite2D.Type.Mask ? 1 : 0;
				float glow = id.glowMode.enable ? id.glowMode.glowRadius : 0;

				GLExtended.color = id.color;

				GL.MultiTexCoord3(1, glow, ratio, type);
	
				Universal.Sprite.MultiPass.Draw(id.spriteMeshObject, id.spriteRenderer, position + id.lightSpriteTransform.position, scale, rot);
			}
		}

		static public class Simple {
			
			static public void Draw(LightSprite2D id, Camera camera) {
				if (id.GetSprite() == null) {
					return;
				}

				if (id.InCamera(camera) == false) {
					return;
				}

				Material material = Lighting2D.materials.GetLightSprite();

				if (material == null) {
					return;
				}

				Vector2 position = id.transform.position;
				position.x -= camera.transform.position.x;
				position.y -= camera.transform.position.y;

				Vector2 scale = id.transform.lossyScale;
				scale.x *= id.lightSpriteTransform.scale.x;
				scale.y *= id.lightSpriteTransform.scale.y;

				float rot = id.lightSpriteTransform.rotation;
				if (id.lightSpriteTransform.applyRotation) {
					rot += id.transform.rotation.eulerAngles.z;
				}

				Sprite sprite = id.GetSprite();
				if (sprite == null) {
					return;
				}

				Texture2D texture = sprite.texture;
				if (texture == null) {
					return;
				}
	
				float ratio = (float)texture.width / (float)texture.height;
				float type = id.type == LightSprite2D.Type.Mask ? 1 : 0;
				float glow = id.glowMode.enable ? id.glowMode.glowRadius : 0;

				material.mainTexture = texture;
				material.SetPass(0);

				GL.Begin (GL.QUADS);

				GLExtended.color = id.color;

				GL.MultiTexCoord3(1, glow, ratio, type);
	
				Universal.Sprite.MultiPass.Draw(id.spriteMeshObject, id.spriteRenderer, position + id.lightSpriteTransform.position, scale, rot);

				GL.End ();
				
				material.mainTexture = null;
			}
		}

	}
}
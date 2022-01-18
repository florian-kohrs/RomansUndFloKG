using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube2D
{

    public Cube2D(Vector2 bottomLeftAnchor, float width, float height)
    {
        this.bottomLeftAnchor = bottomLeftAnchor;
        this.width = width; 
        this.height = height;
        Width = new Vector2(width, 0);
        Height = new Vector2(0, height);
    }

    protected Vector2 bottomLeftAnchor;

    protected float width;

    protected float height;

    protected Vector2 Width;

    protected Vector2 Height;

    protected Vector2 BottomRight => bottomLeftAnchor + Width;

    protected Vector2 TopLeft => bottomLeftAnchor + Height;

    protected Vector2 TopRight => BottomRight + Height;

    protected Vector2 Center => bottomLeftAnchor + new Vector2(width / 2, height / 2);

    protected Material mat;

    protected GameObject box; 

    public void Create()
    {
        Display();
    }

    protected void CreateBox()
    {
        box = GameObject.CreatePrimitive(PrimitiveType.Cube);
        AdjustCube();
    }

    protected GameObject Box
    {
        get
        {
            if(box == null)
            {
                CreateBox();
            }
            return box;
        }
    }

    protected void AdjustCube()
    {
        box.transform.localScale = new Vector3(width,height,1);
        box.transform.localPosition = Center;
        box.GetOrAddComponent<BoxCollider>();
    }


    protected virtual void Display()
    {
        CreateBox();
    }

}
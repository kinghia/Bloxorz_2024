using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float rotationPeriod = 0.3f;		// thời gian để hoàn thành một lần quay
	Vector3 scale;							// kích thước Cube

	public bool _isRotate = false;					// kiểm tra trạng thái quay
	float directionX = 0;					// hướng quay x
	float directionZ = 0;					// hướng quay z

	float startAngleRad = 0;				// góc giữa mặt phẳng ngang và trọng tâm của Cube trước khi quay
	Vector3 startPos;						// vị trí bắt đầu của Cube trước khi quay
	float rotationTime = 0;					// thời gian trôi qua trong quá trình quay
	float radius = 1;						// bán kính quỹ đạo của Cube khi quay (tính từ trọng tâm)
	Quaternion fromRotation;				// Quaternion biểu diễn góc quay trước khi quay
	Quaternion toRotation;					// Quaternion biểu diễn góc quay sau khi quay

	void Start ()
	{
		// lấy kích thước của Cube. Để tính toán quỹ đạo và bán kính quay
		scale = transform.lossyScale;
		Debug.Log ("[x, y, z] = [" + scale.x + ", " + scale.y + ", " + scale.z + "]");

	}

	void Update () 
	{
		float x = 0;
		float y = 0;

		x = Input.GetAxisRaw ("Horizontal");        // a-d
		if (x == 0) 
		{
			y = Input.GetAxisRaw ("Vertical");      // s-w
		}


		// nếu có phím nhập vào và Cube chưa quay
		if ((x != 0 || y != 0) && !_isRotate) 
		{
			directionX = -x;																// thiết lập hướng quay (x,y phải = 0)
			directionZ = y;																// thiết lập hướng quay (x,y phải = 0)
			startPos = transform.position;												// giữ tọa độ trước khi quay
			fromRotation = transform.rotation;											// lưu góc quay hiện tại
			transform.Rotate (directionZ * 90, 0, directionX * 90, Space.World);		// xoay 90 độ theo chiều quay
			toRotation = transform.rotation;											// lưu góc quay mới
			transform.rotation = fromRotation;											// trả góc quay Cube về góc quay trước đó
			setRadius();																// tính bán kính quay
			rotationTime = 0;															// đặt time trôi trong quá trình xoay = 0
			_isRotate = true;															// đánh dấu trạng thái đang quay
		}
	}

	void FixedUpdate() 
	{
		if (_isRotate) 
		{
			rotationTime += Time.fixedDeltaTime;									// time tăng dần với mỗi bước vật lý
			float ratio = Mathf.Lerp(0, 1, rotationTime / rotationPeriod);			// tính tỷ lệ hoàn thành của vòng quay bằng hàm Lerp

			// chuyển động
			float thetaRad = Mathf.Lerp(0, Mathf.PI / 2f, ratio);					// góc quay hiện tại (tính bằng radian)
			float distanceX = -directionX * radius * 
								(Mathf.Cos (startAngleRad) - Mathf.Cos (startAngleRad + thetaRad));		// khoảng cách di chuyển theo X
			float distanceY = radius * 
								(Mathf.Sin(startAngleRad + thetaRad) - Mathf.Sin (startAngleRad));		// khoảng cách di chuyển theo Y
			float distanceZ = directionZ * radius * 
								(Mathf.Cos (startAngleRad) - Mathf.Cos (startAngleRad + thetaRad));		// khoảng cách di chuyển theo Z
			transform.position = new Vector3(startPos.x + distanceX, 
											startPos.y + distanceY, 
											startPos.z + distanceZ);				// đặt vị trí mới

			// xoay
			transform.rotation = Quaternion.Lerp(fromRotation, toRotation, ratio);	// đặt góc xoay hiện tại

			// khởi tạo từng tham số khi kết thúc chuyển động / xoay
			if (ratio == 1) 
			{
				_isRotate = false;
				directionX = 0;
				directionZ = 0;
				rotationTime = 0;
			}
		}
	}

	void setRadius() {

		Vector3 dirVec = new Vector3(0, 0, 0);			// hướng di chuyển
		Vector3 nomVec = Vector3.up;					// (0,1,0)

		// chuyển đổi hướng di chuyển thành vector
		if (directionX != 0) 							// di chuyển theo hướng X
		{
			dirVec = Vector3.right;						// (1,0,0)
		} 
		else if (directionZ != 0) 						// di chuyển theo hướng Z
		{
			dirVec = Vector3.forward;					// (0,0,1)
		} 

		// Tính bán kính và góc bắt đầu của hướng chuyển động từ tích bên trong của vectơ chỉ hướng chuyển động và hướng object
		// Nếu di chuyển theo trục X
		if (Mathf.Abs (Vector3.Dot (transform.right, dirVec)) > 0.99) 						// hướng chuyển động là hướng X object
		{
			if (Mathf.Abs (Vector3.Dot (transform.up, nomVec)) > 0.99) 
			{																				// trục Y Global là hướng Y object
				radius = Mathf.Sqrt(Mathf.Pow(scale.x/2f,2f) + Mathf.Pow(scale.y/2f,2f));	// bán kính xoay vòng
				startAngleRad = Mathf.Atan2(scale.y, scale.x);								// góc của trọng tâm trước khi quay so với mặt phẳng nằm ngang
			} 
			else if (Mathf.Abs (Vector3.Dot (transform.forward, nomVec)) > 0.99) 
			{																				// trục Y Global là hướng Z object
				radius = Mathf.Sqrt(Mathf.Pow(scale.x/2f,2f) + Mathf.Pow(scale.z/2f,2f));
				startAngleRad = Mathf.Atan2(scale.z, scale.x);
			}
		} 

		// Nếu di chuyển theo trục Y
		else if (Mathf.Abs (Vector3.Dot (transform.up, dirVec)) > 0.99) 					// hướng chuyển động là hướng Y pbject
		{
			if (Mathf.Abs (Vector3.Dot (transform.right, nomVec)) > 0.99) 					// trục Y Global là hướng X object
			{
				radius = Mathf.Sqrt(Mathf.Pow(scale.y/2f,2f) + Mathf.Pow(scale.x/2f,2f));
				startAngleRad = Mathf.Atan2(scale.x, scale.y);
			} 
			else if (Mathf.Abs (Vector3.Dot (transform.forward, nomVec)) > 0.99) 			// trục Y Global là hướng Z object
			{
				radius = Mathf.Sqrt(Mathf.Pow(scale.y/2f,2f) + Mathf.Pow(scale.z/2f,2f));
				startAngleRad = Mathf.Atan2(scale.z, scale.y);
			}
		} 
		
		// Nếu di chuyển theo hướng Z
		else if (Mathf.Abs (Vector3.Dot (transform.forward, dirVec)) > 0.99) 				// Hướng chuyển động là hướng Z object
		{
			if (Mathf.Abs (Vector3.Dot (transform.right, nomVec)) > 0.99) 					// trục Y Global là hướng X object
			{					
				radius = Mathf.Sqrt(Mathf.Pow(scale.z/2f,2f) + Mathf.Pow(scale.x/2f,2f));
				startAngleRad = Mathf.Atan2(scale.x, scale.z);
			} 
			else if (Mathf.Abs (Vector3.Dot (transform.up, nomVec)) > 0.99) 				// trục Y Global là hướng Y object
			{				
				radius = Mathf.Sqrt(Mathf.Pow(scale.z/2f,2f) + Mathf.Pow(scale.y/2f,2f));
				startAngleRad = Mathf.Atan2(scale.y, scale.z);
			}
		}
		Debug.Log (radius + ", " + startAngleRad);
	}

	public bool isRotating
	{
		get { return _isRotate; }
	}

}

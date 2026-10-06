using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Checkbox : MonoBehaviour
{
    public Sprite Check;  // Sprite to be used for "Correct" state
    public Sprite Cross;  // Sprite to be used for "Wrong" state

    private Image m_image;  // Reference to the UI Image component
    private bool m_animationCompleted;  // Flag to track if animation is completed
    private float m_FillAmount;  // Variable to track the fill amount of the image
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_FillAmount = 0;
        m_animationCompleted = false;
        m_image = gameObject.GetComponentInChildren<Image>();  // Get the Image component from the children
        m_image.fillAmount = m_FillAmount;
        CustomizeAnimation();  // Customize the animation properties
    }

    // Update is called once per frame
    void Update()
    {
        // Optional: You can add additional logic here if needed in each frame
    }

    // Method to mark the checkbox as correct
    public void Correct()
    {
        CustomizeAnimation();
        m_image.sprite = Check;  // Set the sprite to "Check"
        audioManager.PlaySFX(audioManager.correct_Opt);
        StartCoroutine(FillingEffect());  // Start the filling animation coroutine
    }

    // Method to mark the checkbox as wrong
    public void Wrong()
    {
        CustomizeAnimation();
        m_image.sprite = Cross;  // Set the sprite to "Cross"
        audioManager.PlaySFX(audioManager.wrong_Opt);
        StartCoroutine(FillingEffect());  // Start the filling animation coroutine
    }

    // Method to clear the checkbox
    public void Clear()
    {
        m_FillAmount = 0;
        m_image.fillAmount = m_FillAmount;  // Reset the fill amount of the image
        m_animationCompleted = false;
    }

    // Returns whether the animation has been completed
    public bool AnimationCompleted()
    {
        return m_animationCompleted;
    }

    // Coroutine for the filling effect animation
    IEnumerator FillingEffect()
    {
        while (m_FillAmount < 1)
        {
            m_FillAmount += 0.05f;  // Increment the fill amount
            m_image.fillAmount = m_FillAmount;  // Update the image's fill amount
            yield return null;  // Wait for the next frame
        }

        m_animationCompleted = true;  // Mark the animation as completed
    }

    // Customizes the fill method and fill origin of the image to create a dynamic effect
    private void CustomizeAnimation()
    {
        int fillMethod = 1;  // Fill method is set to one (can be customized as needed)
        int origin = (int)Random.Range(0, 3);  // Randomly selects a fill origin between 0 and 3

        m_image.fillMethod = (Image.FillMethod)fillMethod;  // Set the fill method
        m_image.fillOrigin = origin;  // Set the fill origin
    }
}

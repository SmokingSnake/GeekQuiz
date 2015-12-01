using UnityEngine;
using System.Collections;

public class AutoScreen : MonoBehaviour 
{
    public bool limitarTela;
    public GameObject Ibagem;

	void Start () 
    {
#if UNITY_EDITOR
        limiteTela();
#elif UNITY_ANDROID
        limiteTela();
#endif
        //infoConfig();      
	}

    public void limiteTela() //Checa o tamanho da tela baseado na largura e define se é preciso ou não limitar o tamanho da tela.
    {
        float WidthInches = Screen.width / Screen.dpi;
        float HeightInches = Screen.height / Screen.dpi;

        if (WidthInches <= 3.75f) limitarTela = false;
        else limitarTela = true;

        //if (!limitarTela) Ibagem.GetComponent<UIStretch>().enabled = true;
        //print("limitarTela: " + limitarTela);
        //print("WidthInches: " + WidthInches);
        //print("HeightInches: " + HeightInches);
    }

    public void infoConfig() //Checa todas as configurações possíveis.
    {
        print("screenDPI:" + Screen.dpi);
        print("screenWidth:" + Screen.width);
        print("screenHeight:" + Screen.height);
        print("currentResolution:" + Screen.currentResolution);
        print("orientation:" + Screen.orientation);
        print("deviceModel:" + SystemInfo.deviceModel);
        print("deviceName:" + SystemInfo.deviceName);
        print("deviceType:" + SystemInfo.deviceType);
        print("deviceUniqueIdentifier:" + SystemInfo.deviceUniqueIdentifier);
        print("graphicsDeviceID:" + SystemInfo.graphicsDeviceID);
        print("graphicsDeviceName:" + SystemInfo.graphicsDeviceName);
        print("graphicsDeviceVendor:" + SystemInfo.graphicsDeviceVendor);
        print("graphicsDeviceVendorID:" + SystemInfo.graphicsDeviceVendorID);
        print("graphicsDeviceVersion:" + SystemInfo.graphicsDeviceVersion);
        print("graphicsMemorySize:" + SystemInfo.graphicsMemorySize);
        print("graphicsPixelFillrate:" + SystemInfo.graphicsPixelFillrate);
        print("graphicsShaderLevel:" + SystemInfo.graphicsShaderLevel);
        print("maxTextureSize:" + SystemInfo.maxTextureSize);
        print("npotSupport:" + SystemInfo.npotSupport);
        print("operatingSystem:" + SystemInfo.operatingSystem);
        print("processorCount:" + SystemInfo.processorCount);
        print("processorType:" + SystemInfo.processorType);
        print("supportedRenderTargetCount:" + SystemInfo.supportedRenderTargetCount);
        print("supports3DTextures:" + SystemInfo.supports3DTextures);
        print("supportsAccelerometer:" + SystemInfo.supportsAccelerometer);
        print("supportsComputeShaders:" + SystemInfo.supportsComputeShaders);
        print("supportsGyroscope:" + SystemInfo.supportsGyroscope);
        print("supportsImageEffects:" + SystemInfo.supportsImageEffects);
        print("supportsInstancing:" + SystemInfo.supportsInstancing);
        print("supportsLocationService:" + SystemInfo.supportsLocationService);
        print("supportsRenderTextures:" + SystemInfo.supportsRenderTextures);
        print("supportsRenderToCubemap:" + SystemInfo.supportsRenderToCubemap);
        print("supportsShadows:" + SystemInfo.supportsShadows);
        print("supportsStencil:" + SystemInfo.supportsStencil);
        print("supportsVertexPrograms:" + SystemInfo.supportsVertexPrograms);
        print("supportsVibration:" + SystemInfo.supportsVibration);
        print("systemMemorySize:" + SystemInfo.systemMemorySize);
    }
}

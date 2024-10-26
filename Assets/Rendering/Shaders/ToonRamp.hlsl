void ToonShading_float(in UnityTexture2D albedoTexture, in float2 uv, in float3 surfaceNormal, in float3 clipSpacePos, in float3 worldPos, in float3 viewDirection,
    in float3 diffuseTint, in float3 specularTint, in float specularSmoothness, in float pointLightSpecularSmoothness, in float ambientStrength,
    in float lightStepOffset, in float stepSmoothness, in float pointLightStepSmoothness, out float3 diffuseAndSpecularLighting, out float3 fresnelMask)
    {
        //INTIALIZATION
    #ifdef SHADERGRAPH_PREVIEW
        diffuseAndSpecularLighting = float3(0.5,0.5,0);
        fresnelMask = float3(0.0, 0.0, 0.0);
    #else
    #if SHADOWS_SCREEN
        half4 shadowCoord = ComputeScreenPos(clipSpacePos);
    #else
        half4 shadowCoord = TransformWorldToShadowCoord(worldPos);
    #endif 
    #if _MAIN_LIGHT_SHADOWS_CASCADE || _MAIN_LIGHT_SHADOWS
        Light light = GetMainLight(shadowCoord);
    #else
        Light light = GetMainLight();
    #endif

        //ADDITIONAL POINT LIGHTS
        float3 extraLights;
        int pixelLightCount = GetAdditionalLightsCount();
        float3 additionalDiffuseLight = float3(0.0, 0.0, 0.0);
        float3 additionalSpecularLight = float3(0.0, 0.0, 0.0);
        for (int j = 0; j < pixelLightCount; ++j) {
            Light aLight = GetAdditionalLight(j, worldPos, half4(1, 1, 1, 1));
            //DIFFUSE   
            float3 currentDiffuse = saturate(dot(aLight.direction, surfaceNormal));
            float3 currentAttenuatedDiffuseLight = currentDiffuse;
            float3 currentSteppedDiffuse = smoothstep(lightStepOffset, lightStepOffset + pointLightStepSmoothness, currentAttenuatedDiffuseLight * aLight.distanceAttenuation);
            additionalDiffuseLight += currentSteppedDiffuse * diffuseTint * aLight.color;

            //SPECULAR
            float3 currentN = normalize(aLight.direction + viewDirection);
            float3 currentSpecularLight = pow(saturate(dot(currentN, surfaceNormal) * dot(aLight.direction, surfaceNormal)), pointLightSpecularSmoothness);
            float3 currentAttenuatedAndMaskedSpecular = currentSpecularLight * aLight.shadowAttenuation * aLight.distanceAttenuation * currentAttenuatedDiffuseLight;
            float3 currentSteppedSpecular = smoothstep(lightStepOffset, lightStepOffset + pointLightStepSmoothness, currentAttenuatedAndMaskedSpecular);
            additionalSpecularLight += currentSteppedSpecular * ((specularTint + aLight.color) / 2);

            aLight.distanceAttenuation = 0;
        }

        //DIFFUSE LIGHT FOR DIRECTIONAL LIGHT
        float3 albedoTextureColor = SAMPLE_TEXTURE2D_LOD(albedoTexture.tex, albedoTexture.samplerstate, uv, 0).xyz;

        float3 diffuseLight = dot(surfaceNormal, light.direction);
        float3 attenuatedDiffuseLight = saturate(diffuseLight * light.shadowAttenuation * light.distanceAttenuation);
        float3 steppedDiffuse = smoothstep(lightStepOffset, lightStepOffset + stepSmoothness, attenuatedDiffuseLight);
        //Apply ambient light to the dark parts of the diffuse
        steppedDiffuse = max(ambientStrength, steppedDiffuse);
        float3 finalDiffuse = steppedDiffuse * diffuseTint * light.color * albedoTextureColor;

        //SPECULAR LIGHT FOR DIRECTIONAL LIGHT
        float3 n = normalize(light.direction + viewDirection);
        float3 specularLight = pow(saturate(dot(n, surfaceNormal)), specularSmoothness);
        float3 attenuatedAndMaskedSpecularLight = specularLight * light.shadowAttenuation * light.distanceAttenuation * attenuatedDiffuseLight;
        float3 combinedSpecular = attenuatedAndMaskedSpecularLight;
        float3 steppedSpecular = smoothstep(lightStepOffset, lightStepOffset + stepSmoothness, combinedSpecular);
        float3 finalSpecular = steppedSpecular * specularTint * light.color;

        fresnelMask = attenuatedDiffuseLight;
        diffuseAndSpecularLighting = finalDiffuse + finalSpecular + additionalDiffuseLight + additionalSpecularLight;
        //diffuseAndSpecularLighting = additionalDiffuseLight;
    #endif
    }
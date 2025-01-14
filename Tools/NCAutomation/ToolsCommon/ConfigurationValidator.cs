﻿//  Copyright (c) 2021 Alachisoft
//  
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//  
//     http://www.apache.org/licenses/LICENSE-2.0
//  
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License
using System;
using System.Collections.Generic;
using System.Text;

using Alachisoft.NCache.Common.Configuration;

using System.Reflection;

namespace Alachisoft.NCache.Tools.Common
{
    public class ConfigurationValidator
    {
        private bool _isLocal = false;

        public ConfigurationValidator()
        {

        }

        public bool ValidateConfiguration(object[] configuration)
        {

            if (configuration != null)
            {
                foreach (object cfgObject in configuration)
                {
                    ValidateSingleCacheConfiguration(cfgObject);
                }
            }
            return true;
        }

        public bool ValidateSingleCacheConfiguration(object cfgObject)
        {
            string rootXmlStr = null;
            Type type = cfgObject.GetType();
            object[] cfgObjCustomAttribs = type.GetCustomAttributes(true);

            if (cfgObjCustomAttribs != null && cfgObjCustomAttribs.Length > 0)
            {
                for (int i = 0; i < cfgObjCustomAttribs.Length; i++)
                {
                    ConfigurationRootAttribute rootAttrib = cfgObjCustomAttribs[i] as ConfigurationRootAttribute;
                    if (rootAttrib != null)
                    {
                        rootXmlStr = rootAttrib.RootSectionName;
                    }
                }
            }
            return ValidateConfigurationSection(cfgObject, rootXmlStr, 1);
        }

        private bool ValidateConfigurationSection(Object configSection, string sectionName, int indent)
        {
            //string strPropertyValue = "";
            string endStr = "\r\n";
            string preStr = "".PadRight(indent * 2);

            StringBuilder sb = new StringBuilder(preStr + "<" + sectionName);

            Type type = configSection.GetType();

            PropertyInfo[] propertiesInfo = type.GetProperties();

            if (propertiesInfo != null && propertiesInfo.Length > 0)
            {
                for (int i = 0; i < propertiesInfo.Length; i++)
                {
                    PropertyInfo property = propertiesInfo[i];
                    object[] customAttribs = property.GetCustomAttributes(true);

                    if (customAttribs != null && customAttribs.Length > 0)
                    {
                        for (int j = 0; j < customAttribs.Length; j++)
                        {
                            ConfigurationAttributeAttribute attrib = customAttribs[j] as ConfigurationAttributeAttribute;
                            if (attrib != null)
                            {
                                Object propertyValue = property.GetValue(configSection, null);
                                //string appendedText = attrib.AppendedText != null ? attrib.AppendedText : "";

                                if (sectionName != null && sectionName == "cache-topology" && propertyValue is string)
                                {
                                    if (propertyValue is string)
                                        if ((string)propertyValue == "local-cache")
                                            _isLocal = true;
                                }

                                if (propertyValue == null && attrib.IsRequired)
                                {
                                    throw new Exception("Error: " + attrib.AttributeName + " attribute is missing in the specified configuration.");
                                }
                                //else if (propertyValue != null)
                                //{
                                //    //strPropertyValue = propertyValue.ToString();
                                //    //if (sectionName == "attribute" && strPropertyValue.Contains("<"))
                                //    //{
                                //    //    strPropertyValue = strPropertyValue.Replace("<", "&lt;");
                                //    //    strPropertyValue = strPropertyValue.Replace(">", "&gt;");
                                //    //}
                                //    string value = propertyValue.ToString();

                                //    if (property.DeclaringType == typeof(Boolean))
                                //    {
                                //        value = value.ToLower();
                                //    }

                                //    sb.Append(" " + attrib.AttributeName + "=\"" + value + appendedText + "\"");
                                //}
                                //else
                                //{
                                //    sb.Append(" " + attrib.AttributeName + "=\"\"");
                                //}
                            }
                        }
                    }
                }
            }

            //bool subsectionsFound = false;
            //bool firstSubSection = true;
            //StringBuilder comments = null;

            //get xml string for sub-sections if exists
            if (propertiesInfo != null && propertiesInfo.Length > 0)
            {
                for (int i = 0; i < propertiesInfo.Length; i++)
                {
                    PropertyInfo property = propertiesInfo[i];
                    object[] customAttribs = property.GetCustomAttributes(true);

                    if (customAttribs != null && customAttribs.Length > 0)
                    {
                        for (int j = 0; j < customAttribs.Length; j++)
                        {
                            //ConfigurationCommentAttribute commentAttrib = customAttribs[j] as ConfigurationCommentAttribute;
                            //if (commentAttrib != null)
                            //{
                            //    Object propertyValue = property.GetValue(configSection, null);
                            //    if (propertyValue != null)
                            //    {
                            //        string propStr = propertyValue as string;
                            //        if (!string.IsNullOrEmpty(propStr))
                            //        {
                            //            if (comments == null)
                            //            {
                            //                comments = new StringBuilder();
                            //            }
                            //            comments.AppendFormat("{0}<!--{1}-->{2}", preStr, propStr, endStr);
                            //        }
                            //    }
                            //}

                            ConfigurationSectionAttribute attrib = customAttribs[j] as ConfigurationSectionAttribute;
                            if (attrib != null)
                            {
                                Object propertyValue = property.GetValue(configSection, null);
                                if (propertyValue != null)
                                {
                                    //subsectionsFound = true;
                                    //if (firstSubSection)
                                    //{
                                    //    sb.Append(">" + endStr);
                                    //    firstSubSection = false;
                                    //}
                                    if (propertyValue.GetType().IsArray)
                                    {
                                        Array array = propertyValue as Array;
                                        Object actualSectionObj;
                                        for (int k = 0; k < array.Length; k++)
                                        {
                                            actualSectionObj = array.GetValue(k);

                                            if (actualSectionObj != null)
                                            {
                                                ValidateConfigurationSection(actualSectionObj, attrib.SectionName, indent + 1);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ValidateConfigurationSection(propertyValue, attrib.SectionName, indent + 1);
                                    }
                                }
                                else if (propertyValue == null && attrib.IsRequired)
                                {
                                    if (attrib.SectionName is string)
                                        if ((string)attrib.SectionName == "data-replication" && !_isLocal)
                                            throw new Exception("Error: " + attrib.SectionName + " section is missing in the specified configuration.");
                                }
                            }
                        }
                    }
                }
            }

            //if (subsectionsFound)
            //    sb.Append(preStr + "</" + sectionName + ">" + endStr);
            //else
            //    sb.Append("/>" + endStr);

            //string xml = string.Empty;
            //if (comments != null)
            //{
            //    xml = comments.ToString() + sb.ToString();
            //}
            //else
            //{
            //    xml = sb.ToString();
            //}

            return true;
        }
    }
}

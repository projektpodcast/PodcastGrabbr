<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="xml" indent="yes"/>

  <xsl:param name="fileName" />
  <xsl:param name="updates" select="document($fileName)" />
  <xsl:variable name="updateShows" select="$updates/podcasts/show" />
  <xsl:variable name="updateIds" select="$updates/podcasts" />


  <!--<xsl:variable name="showidcounter" select="0" />-->

  <xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()"/>
    </xsl:copy>
  </xsl:template>

  <xsl:template match="podcasts">
    <xsl:copy>
      <xsl:apply-templates select="$updateShows"/>
      <xsl:apply-templates select="show" />
    </xsl:copy>
  </xsl:template>


  <xsl:template match="//show">
   
     <xsl:copy>





      <xsl:choose>
        <xsl:when test="@sid != 'new'">
          <xsl:variable name="i" select="count(preceding-sibling::show)" />
          <xsl:attribute name="sid">
            <xsl:value-of select="generate-id(node())"/>
          </xsl:attribute>
          <xsl:apply-templates select="child::*"/>
        </xsl:when>
        <xsl:otherwise>
          <xsl:variable name="i" select="count(preceding-sibling::show)" />
          <xsl:attribute name="sid">
            <!--<xsl:text>{generate-id(show)}</xsl:text>-->
            <xsl:value-of select="generate-id(node())"/>
          </xsl:attribute>
          <xsl:apply-templates select="child::*"/>
        </xsl:otherwise>
      </xsl:choose>

    </xsl:copy>
    
  </xsl:template>

  <!--<xsl:template match="$updateShows">
    <xsl:copy>
      <xsl:variable name="i" select="count(preceding-sibling::show)" />
      <xsl:attribute name="sid">
        <xsl:value-of select="$i + 1"/>
      </xsl:attribute>
      <xsl:apply-templates select="child::*"/>
    </xsl:copy>
  </xsl:template>-->

  <!--<xsl:template match="//show">
    <xsl:copy>
      <xsl:attribute name="sid">
        <xsl:text>-added</xsl:text>
      </xsl:attribute>
      <xsl:apply-templates select="child::*"/>
    </xsl:copy>
  </xsl:template>-->

  <!--<xsl:template match="@sid[parent::show]">
    <xsl:attribute name="sid">
      <xsl:value-of select="'your value here'"/>
    </xsl:attribute>
  </xsl:template>-->

  <!--<xsl:template match="podcasts/show/@sid">
    <xsl:variable name="testvar" select="count(../following-sibling::show)" />
    <xsl:attribute name="sid">
      <xsl:value-of select="$testvar"/>
    </xsl:attribute>
  </xsl:template>-->

  <!--<xsl:template match="podcasts/show/@sid">
    <xsl:variable name="testvar" select="count(../following-sibling::show)" />
    <xsl:for-each select="../preceding-sibling::show">
      <xsl:variable name="i" select="position()" />
      <xsl:attribute name="sid">
        <xsl:value-of select="$i + 1"/>
      </xsl:attribute>
    </xsl:for-each>
  </xsl:template>-->




</xsl:stylesheet>







<!--<xsl:template match="podcasts">
  -->
<!--<xsl:copy-of select="podcasts"/>-->
<!--
  -->
<!--<xsl:copy-of select="$updateIds"/>-->
<!--

  -->
<!--<xsl:element name="podcasts">
      <xsl:copy-of select="$updateShows"/>
      <xsl:copy-of select="show"/>
    </xsl:element>-->
<!--


  -->
<!--<xsl:copy>
    <xsl:apply-templates select="$updateShows"/>
    <xsl:apply-templates select="show"></xsl:apply-templates>
    </xsl:copy>-->
<!--

  -->
<!--<xsl:copy-of select="podcasts/node()"/>-->
<!--

  <xsl:copy>
    -->
<!--<xsl:variable name="showidcounter" select="0" />
      <xsl:apply-templates select="$updateIds/node()"/>
      <xsl:apply-templates select="podcasts/node()" />-->
<!--
    <xsl:apply-templates select="$updateShows"/>
    <xsl:apply-templates select="show" />
  </xsl:copy>
  -->
<!--<xsl:apply-templates select="show/@sid"/>-->
<!--
  -->
<!--<xsl:apply-templates select="@sid[parent::show]"/>-->
<!--
</xsl:template>-->






<!--<xsl:variable name="testvar" select="count(/podcasts/node())"/>-->
<!--<xsl:for-each select="podcasts/child::*">
          <xsl:value-of select=""/>
        </xsl:for-each>-->


<!--<xsl:variable name="showidcounter" select="count(following-sibling::show)" />-->

<!--<xsl:template match="@sid">
    <xsl:variable name="showidcounter" select="count(following-sibling::show)" />
    <xsl:attribute name="sid">

      <xsl:value-of select="213"/>
    
    </xsl:attribute>
    </xsl:template>-->
<!--<xsl:template match="show">
    <xsl:attribute name="sid">
      <xsl:variable name="i" select="$showidcounter + 1" />
      <xsl:value-of select="$i" />
    </xsl:attribute>
  </xsl:template>-->

<!--<xsl:template match="show">
    <xsl:variable name="showidcounter" select="count(following-sibling::show)" />
    <xsl:attribute name="sid">
      <xsl:variable name="i" select="$showidcounter + 1" />
      <xsl:value-of select="je" />
    </xsl:attribute>
  </xsl:template>-->

<!--<xsl:template match="@sid[parent::show]">
    <xsl:variable name="showidcounter" select="count(following-sibling::show)" />
    <xsl:attribute name="sid">
      <xsl:value-of select="assaasd" />
    </xsl:attribute>
  </xsl:template>-->



<!--<xsl:template match="@sid[parent::show]">
    <xsl:variable name="showidcounter" select="count(following-sibling::show)" />

    <xsl:attribute name="sid">
      -->
<!--<xsl:value-of select="count(following-sibling::*)"/>-->
<!--
      <xsl:value-of select="asd"/>
    </xsl:attribute>
  </xsl:template>-->
